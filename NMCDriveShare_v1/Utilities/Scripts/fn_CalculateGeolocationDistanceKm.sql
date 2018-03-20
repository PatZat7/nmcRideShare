CREATE FUNCTION [dbo].[fn_CalculateGeolocationDistanceKm]
(
	@longitude1 DECIMAL(8,5),
	@latitude1 DECIMAL(8,5),
	@longitude2 DECIMAL(8,5),
	@latitude2 DECIMAL(8,5)
)
RETURNS DECIMAL(9,4)
AS
BEGIN
	-- Uses the haversine formula to get the distance between two objects
	-- Source: https://en.wikipedia.org/wiki/Haversine_formula at the "explicit inverse"

	-- Convert degrees into radians
	SET @longitude1 *= (PI() / 180.0);
	SET @longitude2 *= (PI() / 180.0);
	SET @latitude1 *= (PI() / 180.0);
	SET @latitude2 *= (PI() / 180.0);

	-- Apply haversine formula
	-- 1) Inner part
	DECLARE @calculation DECIMAL(8,5) = SQUARE(SIN((@latitude2 - @latitude1) / 2)) +
		(COS(@latitude1) * COS(@latitude2) *
		SQUARE(SIN((@longitude2 - @longitude1) / 2)));

	SET @calculation = SQRT(@calculation);

	-- 2) Outer part
	-- Average of earth's radius in km is 6367.4445 (3956.54658047 in miles)
	SET @calculation = (2 * 6367.4445) * ASIN(@calculation);

	RETURN @calculation
END
