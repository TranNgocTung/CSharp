using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyWebApiApp.Data;
using MyWebApiApp.Models;

namespace MyWebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaisController : ControllerBase
    {
        private readonly MyDbContext _context;

        public LoaisController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var dsLoai = _context.loais.ToList();
            return Ok(dsLoai);  
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var dsLoai = _context.loais.SingleOrDefault(
                lo => lo.MaLoai == id);
            if (dsLoai == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(dsLoai);
            }
        }
        [HttpPost]
        public IActionResult CreateNew(LoaiModel model)
        {
            try
            {
                var loai = new Loai()
                {
                    TenLoai = model.TenLoai
                };
                _context.Add(loai);
                _context.SaveChanges();
                return Ok(loai);
            }
            catch (Exception)
            {
                return BadRequest();

            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateLoaiById(int id, LoaiModel model)
        {
            var loai = _context.loais.SingleOrDefault( lo => lo.MaLoai == id);
            if (loai != null)
            {
                loai.TenLoai = model.TenLoai;
                _context.SaveChanges();
                return Ok(loai);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
