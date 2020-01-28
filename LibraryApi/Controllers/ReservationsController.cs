using LibraryApi.Domain;
using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class ReservationsController : Controller
    {
        LibraryDataContext Context;

        public ReservationsController(LibraryDataContext context)
        {
            Context = context;
        }

        [HttpPost("/reservations")]
        [ValidateModel]
        public async Task<ActionResult> AddAReservation([FromBody] PostReservationRequest request)
        {
            var reservation = new Reservation
            {
                For = request.For,
                Books = string.Join(',', request.Books),
                ReservationCreated = DateTime.Now,
                Status = ReservationStatus.Pending
            };
            Context.Reservations.Add(reservation);
            await Context.SaveChangesAsync();
            //GetBookDetailsResponse response = await BooksMapper.Add(bookToAdd);
            //return CreatedAtRoute("books#getbookbyid", new { id = response.Id }, response);
            var response = MapIt(reservation);
            return CreatedAtRoute("reservations#getbyid", new { id = response.Id }, response);
        }

        [HttpGet("/reservations/{id:int}", Name ="reservations#getbyid")]
        public async Task<ActionResult<GetReservationItemResponse>> GetById(int id)
        {
            return Ok();
        }
        private GetReservationItemResponse MapIt(Reservation reservation)
        {
            var response = new GetReservationItemResponse
            {
                Id = reservation.Id,
                For = reservation.For,
                ReservationCreated = DateTime.Now,
                Status = reservation.Status,
                Books = reservation.Books.Split(',')
                .Select(id=> Url.ActionLink("GetBookById", "Books", new { Id=id})).ToList() //http://localhost:1337/books/1
            };
            return response;
        }
    }
}
