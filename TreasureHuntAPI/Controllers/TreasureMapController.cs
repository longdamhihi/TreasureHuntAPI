using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TreasureHuntAPI.Data;
using TreasureHuntAPI.DTOs;
using TreasureHuntAPI.Models;
using TreasureHuntAPI.Services;

namespace TreasureHuntAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreasureMapController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITreasureSolverService _solver;

        public TreasureMapController(AppDbContext context, ITreasureSolverService solver)
        {
            _context = context;
            _solver = solver;
        }

        [HttpPost]
        public async Task<ActionResult<TreasureMap>> SolveAndSave([FromBody] TreasureMapRequest request)
        {
            var fuel = _solver.CalculateMinimalFuel(request.N, request.M, request.P, request.Matrix);

            var entity = new TreasureMap
            {
                N = request.N,
                M = request.M,
                P = request.P,
                MatrixJson = JsonSerializer.Serialize(request.Matrix),
                MinimalFuel = fuel
            };

            _context.TreasureMaps.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(entity);
        }

        [HttpGet]
        public async Task<ActionResult<List<TreasureMap>>> GetAll()
        {
            return await _context.TreasureMaps.ToListAsync();
        }
    }
}