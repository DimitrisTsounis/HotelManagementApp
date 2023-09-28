-- Insert rows into table 'Hotels' in schema '[dbo]'
INSERT INTO [HotelManagement].[dbo].[Hotels] ( [Name], [Address], [StarRating])
VALUES
('AthensHilton', 'Athens', 5),
('ParisHilton', 'Paris', 5),
( 'KrakowHilton', 'Krakow', 5),
('PlazaHotel', 'Mykonos', 4)
GO

-- Insert rows into table 'Bookings' in schema '[dbo]'
INSERT INTO [HotelManagement].[dbo].[Bookings] ( [HotelId], [CustomerName], [NumberOfPax])
VALUES 
(1, 'dtsounis',3),
(1, 'Jon Doe',4),
(1, 'Doe Family',5),
(1, 'Steve Rogers',12),
(2, 'dtsounis',4),
(2, 'Rick Sanchez',5),
(3, 'Natasha Romanoff',1)
GO