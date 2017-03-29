using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ParcelCalculator.API.Models;

namespace ParcelCalculator.API.Controllers
{
    public class ParcelDimensionController : ApiController
    {
        private ParcelCalculatorAPIContext db = new ParcelCalculatorAPIContext();

        // GET: api/ParcelParser
        public IQueryable<ParcelDimensionDTO> GetParcelDimensions()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var result = from data in db.tblParcelDimensionConfigs
                         select new ParcelDimensionDTO()
                         {
                             Id = data.Id,
                             PackageType = data.PackageType,
                             Length = data.Length,
                             Breadth = data.Breadth,
                             Height = data.Height,
                             Cost = data.Cost
                         };
            return result;
        }

        //public HttpResponseMessage GetParcelDimensions()
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var result = from data in db.tblParcelDimensionConfigs
        //                 select new ParcelDimensionDTO()
        //                 {
        //                     Id = data.Id,
        //                     PackageType = data.PackageType,
        //                     Length = data.Length,
        //                     Breadth = data.Breadth,
        //                     Height = data.Height,
        //                     Cost = data.Cost
        //                 };
        //    return Request.CreateResponse(HttpStatusCode.OK, new { ParcelData = result});
        //}

        // GET: api/ParcelParser/5
        [ResponseType(typeof(tblParcelDimensionConfig))]
        public async Task<IHttpActionResult> GettblParcelDimensionConfig(int id)
        {
            tblParcelDimensionConfig tblParcelDimensionConfig = await db.tblParcelDimensionConfigs.FindAsync(id);
            if (tblParcelDimensionConfig == null)
            {
                return NotFound();
            }

            return Ok(tblParcelDimensionConfig);
        }

        // PUT: api/ParcelParser/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PuttblParcelDimensionConfig(int id, tblParcelDimensionConfig tblParcelDimensionConfig)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblParcelDimensionConfig.Id)
            {
                return BadRequest();
            }

            db.Entry(tblParcelDimensionConfig).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblParcelDimensionConfigExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ParcelParser
        [ResponseType(typeof(tblParcelDimensionConfig))]
        public async Task<IHttpActionResult> PosttblParcelDimensionConfig(tblParcelDimensionConfig tblParcelDimensionConfig)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblParcelDimensionConfigs.Add(tblParcelDimensionConfig);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tblParcelDimensionConfig.Id }, tblParcelDimensionConfig);
        }

        // DELETE: api/ParcelParser/5
        [ResponseType(typeof(tblParcelDimensionConfig))]
        public async Task<IHttpActionResult> DeletetblParcelDimensionConfig(int id)
        {
            tblParcelDimensionConfig tblParcelDimensionConfig = await db.tblParcelDimensionConfigs.FindAsync(id);
            if (tblParcelDimensionConfig == null)
            {
                return NotFound();
            }

            db.tblParcelDimensionConfigs.Remove(tblParcelDimensionConfig);
            await db.SaveChangesAsync();

            return Ok(tblParcelDimensionConfig);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblParcelDimensionConfigExists(int id)
        {
            return db.tblParcelDimensionConfigs.Count(e => e.Id == id) > 0;
        }
    }
}