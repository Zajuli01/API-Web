﻿using API_Web.Contexts;
using API_Web.Contracts;
using API_Web.Model;

namespace API_Web.Repositories;

public class EducationRepository : IEducationRepository
{
    private readonly BookingManagementDBContext _context;
    public EducationRepository(BookingManagementDBContext context)
    {
        _context = context;
    }

    /*
     * <summary>
     * Create a new university
     * </summary>
     * <param name="university">University object</param>
     * <returns>University object</returns>
     */
    public Education Create(Education education)
    {
        try
        {
            _context.Set<Education>().Add(education);
            _context.SaveChanges();
            return education;
        }
        catch
        {
            return new Education();
        }
    }

    /*
     * <summary>
     * Update a university
     * </summary>
     * <param name="university">University object</param>
     * <returns>true if data updated</returns>
     * <returns>false if data not updated</returns>
     */
    public bool Update(Education education)
    {
        try
        {
            _context.Set<Education>().Update(education);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /*
     * <summary>
     * Delete a university
     * </summary>
     * <param name="guid">University guid</param>
     * <returns>true if data deleted</returns>
     * <returns>false if data not deleted</returns>
     */
    public bool Delete(Guid guid)
    {
        try
        {
            var education = GetByGuid(guid);
            if (education == null)
            {
                return false;
            }

            _context.Set<Education>().Remove(education);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /*
     * <summary>
     * Get all universities
     * </summary>
     * <returns>List of universities</returns>
     * <returns>Empty list if no data found</returns>
     */
    public IEnumerable<Education> GetAll()
    {
        return _context.Set<Education>().ToList();
    }

    /*
     * <summary>
     * Get a university by guid
     * </summary>
     * <param name="guid">University guid</param>
     * <returns>University object</returns>
     * <returns>null if no data found</returns>
     */
    public Education? GetByGuid(Guid guid)
    {
        return _context.Set<Education>().Find(guid);
    }

}