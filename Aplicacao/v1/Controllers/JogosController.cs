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
    public class JogosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDbRepositorio<Jogo> _dbRepositorioJogo;
        private readonly IDbRepositorio<Time> _dbRepositorioTime;
        private readonly IDbRepositorio<Jogador> _dbRepositorioJogador;
        private readonly IMeusServicos _servicos;

        public JogosController(IConfiguration configuration, IDbRepositorio<Jogo> dbRepositorioJogo,
            IDbRepositorio<Time> dbRepositorioTime, IDbRepositorio<Jogador> dbRepositorioJogador,
            IMeusServicos servicos)
        {
            _configuration = configuration;
            _dbRepositorioJogo = dbRepositorioJogo;
            _dbRepositorioTime = dbRepositorioTime;
            _dbRepositorioJogador = dbRepositorioJogador;
            _servicos = servicos;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Jogo>> Get()
        {
            return _dbRepositorioJogo.ObterTodos().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<object> Get(int id)
        {
            Jogo jogo = _dbRepositorioJogo.Obter(id);

            if (jogo == null)
            {
                return NotFound();
            }
            
            Time timeCasa = _dbRepositorioTime.Obter(jogo.IdTimeCasa);
            Time timeVisitante = _dbRepositorioTime.Obter(jogo.IdTimeVisitante);
            
            return new { Id = jogo.Id, TimeCasa = timeCasa, TimeVisitante = timeVisitante,
                                    PontuacaoTimeCasa = jogo.PontuacaoTimeCasa,
                                    PontuacaoTimeVisitante = jogo.PontuacaoTimeVisitante};
        }

        [HttpPost]
        public ActionResult<Jogo> Post(Jogo jogo)
        {
            Time timeCasa = _dbRepositorioTime.Obter(jogo.IdTimeCasa);
            Time timeVisitante = _dbRepositorioTime.Obter(jogo.IdTimeVisitante);
            if (!(_servicos.TimeValido(timeCasa) && _servicos.TimeValido(timeVisitante)))
            {
                return UnprocessableEntity();
            }
            _dbRepositorioJogo.Inserir(jogo);
            Jogo novoJogo = _dbRepositorioJogo.ObterTodos().Last();

            return CreatedAtAction(nameof(Get), new { id = novoJogo.Id }, novoJogo);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Jogo jogo)
        {
            if (id != jogo.Id)
            {
                return BadRequest();
            }

            try
            {
                _dbRepositorioJogo.Atualizar(jogo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_dbRepositorioJogo.Obter(id) == null)
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
            if (_dbRepositorioJogo.Obter(id) == null)
            {
                return NotFound();
            }

            _dbRepositorioJogo.Deletar(id);

            return NoContent();
        }
    }
}