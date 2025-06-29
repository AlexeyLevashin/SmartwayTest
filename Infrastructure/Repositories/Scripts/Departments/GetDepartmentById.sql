SELECT
    id AS "Id",
    name AS "Name",
    phone AS "Phone"
FROM departments
WHERE id = @id;
