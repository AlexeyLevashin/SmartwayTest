SELECT  e.id AS Id,
        e.name AS Name,
        e.surname AS Surname,
        e.phone AS Phone,
        e.company_id AS CompanyId,
        p.type AS PassportType,
        p.number AS PassportNumber
FROM employees e
         LEFT JOIN passports p ON p.employee_id = e.id
         LEFT JOIN departments d ON e.department_id = d.id WHERE department_id = @departmentId