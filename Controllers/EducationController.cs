using API_Web.Contracts;
using API_Web.Model;
using API_Web.Repositories;
using API_Web.Utility;
using API_Web.ViewModels.Educations;
using API_Web.ViewModels.Universities;
using Microsoft.AspNetCore.Mvc;

namespace API_Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EducationController : ControllerBase
{
    private readonly IEducationRepository _educationRepository;

    private readonly IMapper<Education, EducationVM> _educationVMMapper;

    private readonly IMapper<University, UniversityVM> _mapper;
    public EducationController(IEducationRepository educationRepository, IMapper<University, UniversityVM> mapper, IMapper<Education, EducationVM> educationMapper)
    {
        _educationRepository = educationRepository;
        _mapper = mapper;
        _educationVMMapper = educationMapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var education = _educationRepository.GetAll();
        if (!education.Any())
        {
            return NotFound();
        }
        var resultConverted = education.Select(_educationVMMapper.Map).ToList();

        return Ok(education);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var education = _educationRepository.GetByGuid(guid);
        if (education is null)
        {
            return NotFound();
        }

        return Ok(education);
    }

    [HttpPost]
    public IActionResult Create(EducationVM educationVM)
    {
        var resultConverted = _educationVMMapper.Map(educationVM);
        var result = _educationRepository.Create(resultConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(EducationVM educationVM)
    {
        var educationup = _educationVMMapper.Map(educationVM);
        var isUpdated = _educationRepository.Update(educationup);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _educationRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}
