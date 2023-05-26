using API_Web.Contracts;
using API_Web.Others;
using Microsoft.AspNetCore.Mvc;

namespace API_Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class GeneralController<TEntity, TEntityVM, TInterfaceEntity> : ControllerBase where TEntity : class
        where TEntityVM : class
        where TInterfaceEntity : class
{
    private readonly IGeneralRepository<TEntity> _generalRepository;
    private readonly IMapper<TEntity, TEntityVM> _mapper;


    protected GeneralController(IGeneralRepository<TEntity> generalRepository, IMapper<TEntity, TEntityVM> mapper)
    {
        _generalRepository = generalRepository;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult Create(TEntityVM entity)
    {
        try
        {
            var objectConverted = _mapper.Map(entity);
            var result = _generalRepository.Create(objectConverted);

            if (result is null)
            {
                return NotFound(new ResponseVM<TEntity>
                {
                    Code = 400,
                    Status = "Failed",
                    Message = "Not Found",
                    Data = result
                }
                );

            }
            return Ok(new ResponseVM<TEntity>
            {
                Code = 200,
                Status = "OK",
                Message = "Success",
                Data = result
            }
          );

        }
        catch (Exception ex)
        {
            return Ok(new ResponseVM<TEntity>
            {
                Code = 500,
                Status = "Erorr",
                Message = ex.Message
            }
          );
        }
    }
    [HttpPut]
    public IActionResult Update(TEntityVM bookingVM)
    {
        try
        {
            var booking = _mapper.Map(bookingVM);
            var isUpdated = _generalRepository.Update(booking);
            if (!isUpdated)
            {
                return NotFound(new ResponseVM<TEntity>
                {
                    Code = 400,
                    Status = "Failed",
                    Message = "Gagal Update Data",
                }
              );
            }

            return Ok(new ResponseVM<TEntity>
            {
                Code = 200,
                Status = "OK",
                Message = "Success"

            }
          );

        }
        catch (Exception ex)
        {
            return Ok(new ResponseVM<TEntity>
            {
                Code = 500,
                Status = "Erorr",
                Message = ex.Message
            }
           );
        }

    }
    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            var isDeleted = _generalRepository.Delete(guid);
            if (!isDeleted)
            {
                return NotFound(new ResponseVM<TEntity>
                {
                    Code = 400,
                    Status = "Failed",
                    Message = "Gagal Delete Data",
                }
              );
            }

            return Ok(new ResponseVM<TEntity>
            {
                Code = 200,
                Status = "OK",
                Message = "Success"

            }
          );
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseVM<TEntity>
            {
                Code = 500,
                Status = "Error",
                Message = ex.Message

            }
      );

        }
    }
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        try
        {
            var Entityresult = _generalRepository.GetByGuid(guid);
            if (Entityresult is null)
            {
                return NotFound(new ResponseVM<TEntity>
                {
                    Code = 400,
                    Status = "Failed",
                    Message = "Not Found",
                    Data = Entityresult
                }
             );
            }
            var bookingConverted = _mapper.Map(Entityresult);

            return Ok(new ResponseVM<TEntityVM>
            {
                Code = 200,
                Status = "OK",
                Message = "Success",
                Data = bookingConverted
            }
             );
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseVM<TEntityVM>
            {
                Code = 500,
                Status = "Erorr",
                Message = ex.Message
            }
             );
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var entities = _generalRepository.GetAll();
            if (!entities.Any())
            {

                return NotFound(new ResponseVM<IEnumerable<TEntity>>
                {
                    Code = 400,
                    Status = "Failed",
                    Message = "Not Found",
                    Data = entities
                }
             );
            }
            var entityConverteds = entities.Select(_mapper.Map).ToList();

            return Ok(new ResponseVM<IEnumerable<TEntityVM>>
            {
                Code = 200,
                Status = "OK",
                Message = "Success",
                Data = entityConverteds
            }
           );

        }
        catch (Exception ex)
        {
            return NotFound(new ResponseVM<TEntityVM>
            {
                Code = 500,
                Status = "Erorr",
                Message = ex.Message
            }
          );

        }
    }
}
