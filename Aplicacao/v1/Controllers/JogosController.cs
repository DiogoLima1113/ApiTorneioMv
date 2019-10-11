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
    public class JogosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDbRepositorio<Jogo> _dbRepositorioJogo;
        private readonly IDbRepositorio<Time> _dbRepositorioTime;
        private readonly IDbRepositorio<Jogador> _dbRepositorioJogador;
        private readonly IServicosTorneio _servicos;

        public JogosController(IConfiguration configuration, IDbRepositorio<Jogo> dbRepositorioJogo,
            IDbRepositorio<Time> dbRepositorioTime, IDbRepositorio<Jogador> dbRepositorioJogador,
            IServicosTorneio servicos)
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
            List<Jogo> jogos = _dbRepositorioJogo.ObterTodos().ToList();

            List<Jogo> jogosComTime = new List<Jogo>();
            foreach (var jogo in jogos)
            {
                jogosComTime.Add(_servicos.GerarJogoComTimes(jogo));
            }

            return jogosComTime;
        }

        [HttpGet("{id}")]
        public ActionResult<object> Get(int id)
        {
            Jogo jogo = _dbRepositorioJogo.Obter(id);

            if (jogo == null)
            {
                return NotFound();
            }
            
            return _servicos.GerarJogoComTimes(jogo);
        }

        [HttpPost]
        public ActionResult<Jogo> Post(Jogo jogo)
        {
            Time timeCasa = _dbRepositorioTime.Obter(jogo.IdTimeCasa);
            Time timeVisitante = _dbRepositorioTime.Obter(jogo.IdTimeVisitante);

            if (!_servicos.JogoValido(jogo))
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