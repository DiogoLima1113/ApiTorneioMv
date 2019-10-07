using System.Collections.Generic;
using System.Linq;
using api_torneio_mv.Dominio.Entidade;
using api_torneio_mv.Dominio.Repositorio.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace api_torneio_mv.Aplicacao.v1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JogadoresController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDbRepositorio<Jogador> _dbRepositorioJogador;
        public JogadoresController(IConfiguration configuration, IDbRepositorio<Jogador> dbRepositorioJogador)
        {
            _configuration = configuration;
            _dbRepositorioJogador = dbRepositorioJogador;
        }

        [HttpGet] //api/v1/jogadores
        public ActionResult<IEnumerable<Jogador>> Get()
        {
            return _dbRepositorioJogador.ObterTodos().ToList();
        }

        [HttpGet("{id}")] //api/v1/jogadores/1
        public ActionResult<Jogador> Get(int id)
        {
            Jogador jogador = _dbRepositorioJogador.Obter(id);

            if (jogador == null)
            {
                return NotFound();
            }

            return jogador;
        }

        [HttpPost]
        public ActionResult<Jogador> Post(Jogador jogador)
        {
            _dbRepositorioJogador.Inserir(jogador);
            Jogador novoJogador = _dbRepositorioJogador.ObterTodos().Last();

            return CreatedAtAction(nameof(Get), new { id = novoJogador.Id }, novoJogador);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Jogador jogador)
        {
            if (id != jogador.Id)
            {
                return BadRequest();
            }

            try
            {
                _dbRepositorioJogador.Atualizar(jogador);
            }
            catch (System.Exception)
            {
                if (_dbRepositorioJogador.Obter(id) == null)
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
            if (_dbRepositorioJogador.Obter(id) == null)
            {
                return NotFound();
            }

            _dbRepositorioJogador.Deletar(id);

            return NoContent();
        }
    }
}