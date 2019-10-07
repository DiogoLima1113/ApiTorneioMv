using System.Collections.Generic;
using System.Linq;
using api_torneio_mv.Dominio.Entidade;
using api_torneio_mv.Dominio.Repositorio.Interface;
using api_torneio_mv.Servico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace api_torneio_mv.Aplicacao.v1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TimesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDbRepositorio<Time> _dbRepositorioTime;
        private readonly IMeusServicos _servicos;

        public TimesController(IConfiguration configuration, IDbRepositorio<Time> dbRepositorioTime,
            IMeusServicos servicos)
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
        public ActionResult<object> Get(int id)
        {
            Time time = _dbRepositorioTime.Obter(id);

            if (time == null)
            {
                return NotFound();
            }
            
            var jogadores = _servicos.ObterJogadoresDoTime(time);
            if (jogadores.Any())
            {
                var timeCompleto = new { Id = time.Id, Nome = time.Nome, 
                                            ListaJogadores = jogadores };

                return timeCompleto;
            }

            return time;
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