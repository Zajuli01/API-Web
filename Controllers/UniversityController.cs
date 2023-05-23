using API_Web.Contracts;
using API_Web.Model;
using API_Web.ViewModels.Educations;
using API_Web.ViewModels.Universities;
using Microsoft.AspNetCore.Mvc;

namespace API_Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UniversityController : ControllerBase
{
    private readonly IUniversityRepository _universityRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper<University, UniversityVM> _mapper;
    private readonly IMapper<Education, EducationVM> _educationVMMapper;
    public UniversityController(IUniversityRepository universityRepository, IEducationRepository educationRepository, IMapper<University, UniversityVM> mapper, IMapper<Education, EducationVM> educationMapper)
    {
        _universityRepository = universityRepository;
        _educationRepository = educationRepository;
        _mapper = mapper;
        _educationVMMapper = educationMapper;
    }


    [HttpGet("WithEducation")]
    public IActionResult GetAllWithEducation()
    {
        var universities = _universityRepository.GetAll();
        if (!universities.Any())
        {
            return NotFound();
        }

        var results = new List<UniversityWithEducationVM>();
        foreach (var university in universities)
        {
            var edu = _educationRepository.GetByGuid(university.Guid);
            //var eduMapped = edu.Select(EducationVM.ToVM));

            var result = new UniversityWithEducationVM
            {
                Guid = university.Guid,
                Code = university.Code,
                Name = university.Name
                //Educations = eduMapped
            };

            results.Add(result);
        }

        return Ok(results);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var universities = _universityRepository.GetAll();
        if (!universities.Any())
        {
            return NotFound();
        }

        /*var univeritiesConverted = new List<UniversityVM>();
        foreach (var university in universities) {
            var result = UniversityVM.ToVM(university);
            univeritiesConverted.Add(result);
        }*/

        var resultConverted = universities.Select(_mapper.Map).ToList();

        return Ok(resultConverted);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var university = _universityRepository.GetByGuid(guid);
        if (university is null)
        {
            return NotFound();
        }

        var data = _mapper.Map(university);

        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(UniversityVM universityVM)
    {
        var universityConverted = _mapper.Map(universityVM);

        var result = _universityRepository.Create(universityConverted);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPut]
    public IActionResult Update(UniversityVM universityVM)
    {
        var UniversityCompored = _mapper.Map(universityVM);
        var isUpdated = _universityRepository.Update(UniversityCompored);
        if (!isUpdated)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _universityRepository.Delete(guid);
        if (!isDeleted)
        {
            return BadRequest();
        }

        return Ok();
    }
}
