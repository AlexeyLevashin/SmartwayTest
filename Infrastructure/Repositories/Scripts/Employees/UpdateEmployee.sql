UPDATE employees
SET name = @name,
    surname = @surname,
    phone = @phone,
    company_id = @companyId,
    department_id = @departmentId
WHERE id = @employeeId
    RETURNING id AS "Id", 
          name AS "Name", 
          surname AS "Surname", 
          phone AS "Phone", 
          company_id AS "CompanyId", 
          department_id AS "DepartmentId";