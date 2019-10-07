using System.Collections.Generic;
using System.Linq;
using api_torneio_mv.Dominio.Entidade;
using api_torneio_mv.Dominio.Repositorio.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace api_torneio_mv.Aplicacao.v1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RelacionamentosTimeJogadorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDbRepositorio<RelacionamentoTimeJogador> _dbRepositorioRelacionamento;
        private readonly IDbRepositorio<Time> _dbRepositorioTime;
        private readonly IDbRepositorio<Jogador> _dbRepositorioJogador;

        public RelacionamentosTimeJogadorController(IConfiguration configuration,
            IDbRepositorio<RelacionamentoTimeJogador> dbRepositorioRelacionamento,
            IDbRepositorio<Time> dbRepositorioTime, IDbRepositorio<Jogador> dbRepositorioJogador)
        {
            _configuration = configuration;
            _dbRepositorioRelacionamento = dbRepositorioRelacionamento;
            _dbRepositorioTime = dbRepositorioTime;
            _dbRepositorioJogador = dbRepositorioJogador;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RelacionamentoTimeJogador>> Get()
        {
            return _dbRepositorioRelacionamento.ObterTodos().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<object> Get(int id)
        {
            RelacionamentoTimeJogador relacionamento = _dbRepositorioRelacionamento.Obter(id);

            if (relacionamento == null)
            {
                return NotFound();
            }
            
            return new { Id = relacionamento.Id, Time = _dbRepositorioTime.Obter(relacionamento.IdTime),
                        Jogador = _dbRepositorioJogador.Obter(relacionamento.IdJogador)};
        }

        [HttpPost]
        public ActionResult<RelacionamentoTimeJogador> Post(RelacionamentoTimeJogador relacionamento)
        {
            _dbRepositorioRelacionamento.Inserir(relacionamento);
            RelacionamentoTimeJogador novoRelacionamento = _dbRepositorioRelacionamento.ObterTodos().Last();

            return CreatedAtAction(nameof(Get), new { id = novoRelacionamento.Id }, novoRelacionamento);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, RelacionamentoTimeJogador relacionamento)
        {
            if (id != relacionamento.Id)
            {
                return BadRequest();
            }

            try
            {
                _dbRepositorioRelacionamento.Atualizar(relacionamento);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_dbRepositorioRelacionamento.Obter(id) == null)
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
            if (_dbRepositorioRelacionamento.Obter(id) == null)
            {
                return NotFound();
            }

            _dbRepositorioRelacionamento.Deletar(id);

            return NoContent();
        }
    }
}