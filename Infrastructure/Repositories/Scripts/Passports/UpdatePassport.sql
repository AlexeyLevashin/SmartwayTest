UPDATE passports
SET type = @type,
    number = @number,
    employee_id = @employeeId
WHERE employee_id = @employeeId
    RETURNING id AS "Id", 
          type AS "Type", 
          number AS "Number", 
          employee_id AS "EmployeeId";
