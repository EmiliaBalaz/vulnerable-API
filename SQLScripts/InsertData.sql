--SELECT *
--  FROM [dbo].[registerUsers]

INSERT INTO [BookStore].[dbo].[Books]([Author], [Name])
VALUES ('Khaled Hosseini','Kite Runner')
     , ('Khaled Hosseini','A thousand splended suns')
     , ('Khaled Hosseini','And the mountains echoed')
     , ('Sun Tzu','The art of war')
     , ('Slavoljub Stankovic','Split')
     , ('Paulo Coelho', 'The alchemist')
     , ('Paulo Coelho', 'Zahir');

INSERT INTO [BookStore].[dbo].[RegisterUsers]([FirstName], [LastName], [Email], [UserName], [Password], [Type])
VALUES ('Admin', 'Admin', 'admin@gmail.com', 'adminadmin', 'dt4gBWb3j3pQ56T1VhEb4g==', 1)
      ,('Emilija', 'Balaz', 'emilija@gmail.com', 'emilija', 'GyqIxtb1Lwcj5zW9faFNmA==', 1)
      ,('Djordje', 'Bozovic', 'djordje@gmail.com', 'djordje', 'nZADDL1JOUX7bbPA9C4DOQ==', 1)
      ,('Korisnik', 'Novi', 'novikorisnik@gmail.com', 'novikorisnik', 'TxOVS8YQQHz0ZuSjsxAXBmQGW0UI51QSiWOmvv0X/LM=', 0)
      ,('Marija', 'Mutuc', 'marija@gmail.com', 'marija', 'm4HRqzehh/otoh0ewr/R1A==', 0)
      ,('string', 'string', 'string', 'string', 'PH6uvWddLjcwoSa+Daz5DQ==', 0)