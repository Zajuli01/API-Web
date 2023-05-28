using System.Net;

namespace API_Web.Others;

public class ResponseVM<TEntity>
{
    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public TEntity? Data { get; set; }

    public ResponseVM<TEntity> Success(TEntity entity)
    {
        return new ResponseVM<TEntity>
        {
            Code = 200,
            Status = "Ok",
            Message = "Success",
            Data = entity
        };

    }
    public ResponseVM<TEntity> Success(string keterangan)
    {
        return new ResponseVM<TEntity>
        {
            Code = 200,
            Status = "Ok",
            Message = keterangan,
        };

    }
    public ResponseVM<TEntity> NotFound(string keterangan)
    {
        return new ResponseVM<TEntity>
        {
            Code = 400,
            Status = "null",
            Message = keterangan
        };
    }
    public ResponseVM<TEntity> NotFound()
    {
        return new ResponseVM<TEntity>
        {
            Code = 400,
            Status = HttpStatusCode.NotFound.ToString(),
            Message = "Data Tidak ada"
        };
    }
    public ResponseVM<TEntity> NotFound(TEntity entity)
    {
        return new ResponseVM<TEntity>
        {
            Code = 400,
            Status = "null",
            Message = "Data Kosong",
            Data = entity
        };
    }
    public ResponseVM<TEntity> Error(string explain)
    {
        return new ResponseVM<TEntity>
        {
            Code = 500,
            Status = "error",
            Message = explain
        };
    }

}
