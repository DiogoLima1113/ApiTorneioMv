using System.Collections.Generic;
using System.Linq;
using ApiTorneioMv.Dominio.Entidade;
using ApiTorneioMv.Dominio.Repositorio.Interface;
using ApiTorneioMv.Servico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApiTorneioMv.Aplicacao.v1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TimesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDbRepositorio<Time> _dbRepositorioTime;
        private readonly IServicosTorneio _servicos;

        public TimesController(IConfiguration configuration, IDbRepositorio<Time> dbRepositorioTime,
            IServicosTorneio servicos)
        {
            _configuration = configuration;
            _dbRepositorioTime = dbRepositorioTime;
            _servicos = servicos;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Time>> Get()
        {
            return _dbRepositorioTime.ObterTodos().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Time> Get(int id)
        {
            Time time = _dbRepositorioTime.Obter(id);

            if (time == null)
            {
                return NotFound();
            }
            
            return _servicos.GerarTimeComJogadores(time);
        }

        [HttpPost]
        public ActionResult<Time> Post(Time time)
        {
            _dbRepositorioTime.Inserir(time);
            Time novoTime = _dbRepositorioTime.ObterTodos().Last();

            return CreatedAtAction(nameof(Get), new { id = novoTime.Id }, novoTime);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Time time)
        {
            if (id != time.Id)
            {
                return BadRequest();
            }

            try
            {
                _dbRepositorioTime.Atualizar(time);
                _servicos.AtualizaJogadoresTime(time);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_dbRepositorioTime.Obter(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_dbRepositorioTime.Obter(id) == null)
            {
                return NotFound();
            }

            _dbRepositorioTime.Deletar(id);

            return NoContent();
        }
    }
}