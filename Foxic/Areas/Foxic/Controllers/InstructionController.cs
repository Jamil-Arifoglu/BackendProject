using Foxic.DAL;
using Foxic.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Foxic.Areas.Foxic.Controllers
{
    [Area("Foxic")]
    public class InstructionController : Controller
    {
        private readonly FoxicDbContext _context;

        public InstructionController(FoxicDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Instruction> instruction = _context.Instructions.AsEnumerable();
            return View(instruction);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Creates(Instruction newInstruction)
        {
            if (!ModelState.IsValid)
            {
                foreach (string message in ModelState.Values.SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage))
                {
                    ModelState.AddModelError("", message);
                }

                return View();
            }
            bool isDuplicated = _context.Instructions.Any(d => d.Drycleaning == newInstruction.Drycleaning && d.Lining == newInstruction.Lining && d.Chlorine == newInstruction.Chlorine && d.Polyester == newInstruction.Polyester);
            if (isDuplicated)
            {
                ModelState.AddModelError("", "You cannot duplicate value");
                return View();
            }
            _context.Instructions.Add(newInstruction);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();
            Instruction instruction = _context.Instructions.FirstOrDefault(c => c.Id == id);
            if (instruction is null) return NotFound();
            return View(instruction);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id, Instruction edited)
        {
            if (id == 0) return NotFound();
            Instruction instruction = _context.Instructions.FirstOrDefault(c => c.Id == id);
            if (instruction is null) return NotFound();

            bool duplicate = _context.Instructions.Any(d => d.Drycleaning == edited.Drycleaning && d.Lining == edited.Lining && d.Chlorine == edited.Chlorine && d.Polyester == edited.Polyester
            && instruction.Drycleaning != edited.Drycleaning && instruction.Lining != edited.Lining && instruction.Chlorine != edited.Chlorine && instruction.Polyester != edited.Polyester
            );
            if (duplicate)
            {
                ModelState.AddModelError("", "You cannot duplicate category name");
                return View();
            }
            instruction.Drycleaning = edited.Drycleaning;
            instruction.Lining = edited.Lining;
            instruction.Chlorine = edited.Chlorine;
            instruction.Polyester = edited.Polyester;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int id)
        {

            if (id == 0) return NotFound();
            Instruction instruction = _context.Instructions.FirstOrDefault(c => c.Id == id);
            if (instruction is null) return NotFound();
            return View(instruction);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            Instruction instruction = _context.Instructions.FirstOrDefault(c => c.Id == id);
            if (instruction is null) return NotFound();
            return View(instruction);
        }


        [HttpPost]
        public IActionResult Delete(int id, Catagory delete)
        {
            if (id == 0) return NotFound();
            Instruction instruction = _context.Instructions.FirstOrDefault(c => c.Id == id);
            if (instruction is null) return NotFound();


            if (id == delete.Id)
            {
                _context.Instructions.Remove(instruction);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(instruction);
        }
    }
}
