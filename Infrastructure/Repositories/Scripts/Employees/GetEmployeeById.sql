SELECT id as "Id",
       name as "Name",
       surname as "Surname",
       phone as "Phone",
       company_id as "CompanyId",
       department_id as "DepartmentId"
FROM employees WHERE id=@id