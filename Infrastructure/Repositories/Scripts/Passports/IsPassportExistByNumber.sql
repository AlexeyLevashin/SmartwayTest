SELECT EXISTS(SELECT id
              FROM passports
              WHERE number = @number)