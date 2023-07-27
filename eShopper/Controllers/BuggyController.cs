﻿using eShopper.Errors;
using eShopper.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace eShopper.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _storeContext;

        public BuggyController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _storeContext.Products.Find(42);
            if(thing == null) return NotFound(new ApiResponse(404));

            return Ok();
        }

        [HttpGet("serverError")]
        public ActionResult GetServerError()
        {
            var thing = _storeContext.Products.Find(42);

            var thingToReturn = thing.ToString();
          
            return Ok();
        }

        [HttpGet("badRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badRequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}
