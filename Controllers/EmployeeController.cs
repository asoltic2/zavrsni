using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers;

/// <summary>
/// API Controller for managing employee records.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeController"/> class.
    /// </summary>
    /// <param name="context">The database context for employees.</param>
    public EmployeeController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all employee records.
    /// </summary>
    /// <returns>A list of employee records.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        return await _context.Employees.ToListAsync();
    }

    /// <summary>
    /// Retrieves a specific employee by their ID.
    /// </summary>
    /// <param name="id">The ID of the employee.</param>
    /// <returns>The employee record with the specified ID.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return employee;
    }

    /// <summary>
    /// Creates a new employee record.
    /// </summary>
    /// <param name="employee">The employee record to create.</param>
    /// <returns>The newly created employee record.</returns>
    [HttpPost]
    public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
    }

    /// <summary>
    /// Updates an existing employee record.
    /// </summary>
    /// <param name="id">The ID of the employee to update.</param>
    /// <param name="employee">The updated employee record.</param>
    /// <returns>No content if update is successful; otherwise, an error response.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmployee(int id, Employee employee)
    {
        if (id != employee.EmployeeId)
        {
            return BadRequest();
        }

        _context.Entry(employee).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmployeeExists(id))
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

    /// <summary>
    /// Deletes a specific employee record by their ID.
    /// </summary>
    /// <param name="id">The ID of the employee to delete.</param>
    /// <returns>No content if deletion is successful; otherwise, an error response.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Checks whether an employee exists.
    /// </summary>
    /// <param name="id">The ID of the employee.</param>
    /// <returns><c>true</c> if the employee exists; otherwise, <c>false</c>.</returns>
    private bool EmployeeExists(int id)
    {
        return _context.Employees.Any(e => e.EmployeeId == id);
    }
}
