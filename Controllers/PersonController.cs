using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PercobaanApi1.Models;
namespace PercobaanApi1.Controllers
{
    public class PersonController : Controller
    {
        private string __constr;

        public PersonController(IConfiguration configuration)
        {
            __constr = configuration.GetConnectionString("WebApiDatabase");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/person")]
        public ActionResult<Person> ListPerson()
        {
            PersonContext context = new PersonContext(this.__constr);
            List<Person> ListPerson = context.ListPerson();
            return Ok(ListPerson);
        }

        [HttpPost("api/person_auth"), Authorize]

        public ActionResult<Person> ListPersonWithAuth()
        {
            PersonContext context = new PersonContext(this.__constr);
            List<Person> ListPerson = context.ListPerson();
            return Ok(ListPerson);
        }


        [HttpGet("api/person/{id}")]
        public IActionResult GetPerson(int id)
        {
            PersonContext context = new PersonContext(__constr);
            List<Person> ListPerson = context.ListPerson();
            var person = context.ListPerson().Find(p => p.id_person == id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        public IActionResult GetPersonTR()
        {
            PersonContext context1 = new PersonContext(this.__constr);
            List<Person> ListPerson1 = context1.ListPerson();


            Person idpr = ListPerson1.LastOrDefault();

            int idprs = idpr.id_person;

            var person = context1.ListPerson().Find(p => p.id_person == idprs);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }



        [HttpPost("api/personPost")]
        public IActionResult PostPerson([FromBody] Insert person)
        {


            PersonContext context = new PersonContext(__constr);
            context.AddPerson(person);

            return GetPersonTR();
        }


        [HttpPut("api/person/{id}")]
        public IActionResult PutPerson(int id, [FromBody] Insert person)
        {
            PersonContext context1 = new PersonContext(__constr);
            List<Person> ListPerson = context1.ListPerson();
            var person1 = context1.ListPerson().Find(p => p.id_person == id);

            if (person == null)
            {
                return NotFound();
            }
            else
            {
                PersonContext context = new PersonContext(__constr);
                context.UpdatePerson(person, id);

                return NoContent();
            }


        }

        [HttpDelete("api/person/{id}")]
        public IActionResult DeletePerson(int id)
        {
            PersonContext context = new PersonContext(__constr);
            context.DeletePerson(id);

            return NoContent();
        }

    }
}