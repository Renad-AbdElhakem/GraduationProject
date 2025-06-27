using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Flower_Shop.DTO;
using Smart_Flower_Shop.Models;
using System.Text.Json;

namespace Smart_Flower_Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        public ProductsController(ApplicationDbcontext db)
        {
            _context = db;
        }

        private readonly ApplicationDbcontext _context;



        [HttpGet("GetHomeFlowers")]
        public IActionResult GetFlowers(int pageNumber = 1, int pageSize = 5)
        {
            var totalCount = _context.Products.Count();

            var flowers = _context.Products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(f => new ProductsHomeDto
                {
                    Id = f.ProductId,
                    ImagePath = f.ImagePath,
                    Name = f.Name,
                    Type = f.Type,
                    Price = f.Price,
                    Quantity = f.Quantity
                })
                .ToList();

            var response = new
            {
              
                Items = flowers
            };

            return Ok(response);
        }


        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _context.Products
                .Where(p => p.ProductId == id)
                .Select(p => new ProductDetailsDTO
                {
                    ImagePath= p.ImagePath,
                    Name = p.Name,
                    Type = p.Type,
                    Price = p.Price,
                   Quantity = p.Quantity,
               
                })
                .FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


         






        [HttpPost("AddNewProduct")]
        public async Task<IActionResult> CreateProductWithImage([FromForm] ProductsDTO productDto)
        {
            if (productDto == null)
                return BadRequest("Invalid product data.");

            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == productDto.CatgyId);
            if (category == null)
                return BadRequest("Category ID not found.");

            // 1. حفظ الصورة
            string imageName = null;
            if (productDto.ImageFile != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                imageName = Guid.NewGuid().ToString() + Path.GetExtension(productDto.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, imageName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productDto.ImageFile.CopyToAsync(stream);
                }
            }

            // 2. إنشاء المنتج
            var product = new Products
            {
                Name = productDto.Name,
                Quantity = productDto.Quantity,
                Price = productDto.Price,
                Type = productDto.Type,
                Description = productDto.Description,
                CatgyId = productDto.CatgyId,
                ImagePath = "Images/" + imageName

            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok("Product created successfully.");
        }


     



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(int id, [FromForm] ProdEditionDto productDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if (productDto.Quantity.HasValue)
            {
                product.Quantity += productDto.Quantity.Value;
            }

        
            if (productDto.Price.HasValue)
            {
                product.Price = productDto.Price.Value;
            }


         
            if (productDto.ImageFile != null)
            {
               
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(productDto.ImageFile.FileName);
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);
                var filePath = Path.Combine(uploadsFolder, fileName);

         
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productDto.ImageFile.CopyToAsync(stream);
                }

              
                if (!string.IsNullOrEmpty(product.ImagePath))
                {
                    var oldImagePath = Path.Combine("wwwroot", product.ImagePath);
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }

             
                product.ImagePath = "Images/" + fileName;
            }

     
            await _context.SaveChangesAsync();

            return Ok(new { message = "Product updated successfully." });

        }



        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return NoContent();  // HTTP 204
        }


         [HttpGet("SearchByName")] //search bar 
         public async Task<IActionResult> SearchProducts(string keyword)
         {
             var result = await _context.Products
                 .Where(f => f.Name.Contains(keyword))
                 .Select(f => new
                 {
                     f.ProductId,
                    f.ImagePath,
                     f.Name,
                     f.Type,
                     f.Price,
                     f.Quantity,
                  

                 })
                 .ToListAsync();

             return Ok(result);
         }




        [HttpGet("CareProductsByType")]
        public async Task<IActionResult> CareProducts(string keyword)
        {
            var result = await _context.Products
                .Where(f => f.Type.Contains(keyword))
                .Select(f => new CareProductDto
                {
                   
                    ImagePath= f.ImagePath,
                    Name = f.Name,
                    Description = f.Description
                })
                .ToListAsync();

            return Ok(result);
        }




       









    }
}


