Create Database TravelEase

USE TravelEase

-- Adding proper constraints, uniqueness, and data validations

CREATE TABLE TripCategory
(
  category_id INT IDENTITY(1,1) NOT NULL,
  category_name VARCHAR(50) NOT NULL,
  PRIMARY KEY (category_id)
);

CREATE TABLE Admin
(
  admin_id INT IDENTITY(1,1) NOT NULL,
  username VARCHAR(50) NOT NULL,
  password VARCHAR(50) NOT NULL,
  PRIMARY KEY (admin_id)
);

CREATE TABLE Payment
(
  payment_id INT IDENTITY(1,1) NOT NULL,
  amount INT NOT NULL,
  payment_method VARCHAR(50) NOT NULL,
  payment_status VARCHAR(50) NOT NULL,
  is_refunded bit NOT NULL,
  -- Changed to BOOLEAN
  payment_date DATE NOT NULL,
  transaction_status VARCHAR(50) NOT NULL,
  is_disputed bit NOT NULL,
  -- Changed to BOOLEAN
  dispute_reason TEXT NOT NULL,
  transaction_id VARCHAR(100) NOT NULL,
  payment_gateway VARCHAR(50) NOT NULL,
  failure_reason TEXT NOT NULL,
  fraud_flag bit NOT NULL,
  -- Changed to BOOLEAN
  PRIMARY KEY (payment_id),
  UNIQUE (transaction_id)
);

CREATE TABLE DigitalTravelPass
(
  pass_id INT IDENTITY(1,1) NOT NULL,
  e_ticket VARCHAR(50) NOT NULL,
  hotel_voucher VARCHAR(50) NOT NULL,
  activity_pass VARCHAR(50) NOT NULL,
  generated_on DATE NOT NULL,
  PRIMARY KEY (pass_id)
);

CREATE TABLE ServiceProvider
(
  provider_id INT IDENTITY(1,1) NOT NULL,
  name VARCHAR(100) NOT NULL,
  service_type VARCHAR(50) NOT NULL,
  is_available bit NOT NULL,
  -- Changed to BOOLEAN
  phone VARCHAR(20) NOT NULL,
  is_approved bit NOT NULL,
  -- Changed to BOOLEAN
  registration_date DATE NOT NULL,
  PRIMARY KEY (provider_id)
);

CREATE TABLE ServiceAssignment
(
  assignment_id INT IDENTITY(1,1) NOT NULL,
  status VARCHAR(50) NOT NULL,
  assigned_date DATE NOT NULL,
  is_approved bit NOT NULL,
  -- Changed to BOOLEAN
  PRIMARY KEY (assignment_id)
);

CREATE TABLE TransportProvider
(
  transport_id INT IDENTITY(1,1) NOT NULL,
  transport_type VARCHAR(50) NOT NULL,
  fleet_size INT NOT NULL,
  license_details TEXT NOT NULL,
  capacity_per_vehicle INT NOT NULL,
  provider_id INT NOT NULL,
  PRIMARY KEY (transport_id),
  FOREIGN KEY (provider_id) REFERENCES ServiceProvider(provider_id)
);

CREATE TABLE Guide_
(
  -- Fixed trailing space from ERD
  guide_id INT IDENTITY(1,1) NOT NULL,
  specialization VARCHAR(100) NOT NULL,
  languages_spoken TEXT NOT NULL,
  experience_years INT NOT NULL,
  provider_id INT NOT NULL,
  review INT NOT NULL,
  PRIMARY KEY (guide_id),
  FOREIGN KEY (provider_id) REFERENCES ServiceProvider(provider_id)
);

CREATE TABLE Hotel
(
  hotel_id INT IDENTITY(1,1) NOT NULL,
  total_rooms INT NOT NULL,
  star_rating INT NOT NULL,
  address TEXT NOT NULL,
  wheelchair_accessible bit NOT NULL,
  -- Changed to BOOLEAN
  provider_id INT NOT NULL,
  PRIMARY KEY (hotel_id),
  FOREIGN KEY (provider_id) REFERENCES ServiceProvider(provider_id)
);


CREATE TABLE ProviderAssignment
(
  provider_id INT IDENTITY(1,1) NOT NULL,
  assignment_id INT NOT NULL,
  PRIMARY KEY (provider_id, assignment_id),
  FOREIGN KEY (provider_id) REFERENCES ServiceProvider(provider_id),
  FOREIGN KEY (assignment_id) REFERENCES ServiceAssignment(assignment_id)
);

CREATE TABLE HotelAmenities
(
  amenity_id INT IDENTITY(1,1) NOT NULL,
  amenity_name VARCHAR(50) NOT NULL,
  hotel_id INT NOT NULL,
  PRIMARY KEY (amenity_id),
  FOREIGN KEY (hotel_id) REFERENCES Hotel(hotel_id)
);

CREATE TABLE HotelRoomTypes
(
  room_type_id INT IDENTITY(1,1) NOT NULL,
  room_type_name VARCHAR(50) NOT NULL,
  max_occupancy INT NOT NULL,
  price_per_night INT NOT NULL,
  hotel_id INT NOT NULL,
  PRIMARY KEY (room_type_id),
  FOREIGN KEY (hotel_id) REFERENCES Hotel(hotel_id)
);

CREATE TABLE GuideLanguages
(
  guide_language_id INT IDENTITY(1,1) NOT NULL,
  language VARCHAR(50) NOT NULL,
  guide_id INT NOT NULL,
  PRIMARY KEY (guide_language_id),
  FOREIGN KEY (guide_id) REFERENCES Guide_(guide_id)
);

CREATE TABLE Traveler
(
  traveler_id INT IDENTITY(1,1) NOT NULL,
  name VARCHAR(100) NOT NULL,
  email VARCHAR(100) NOT NULL,
  password VARCHAR(100) NOT NULL,
  age INT NOT NULL,
  nationality VARCHAR(50) NOT NULL,
  registration_date DATE DEFAULT GETDATE() NOT NULL,
  is_approved bit DEFAULT 1 NOT NULL,
  -- Changed to BOOLEAN
  last_login_date DATE DEFAULT GETDATE() NULL,
  -- Fixed trailing space from ERD
  admin_id INT DEFAULT 1 NULL,
  PRIMARY KEY (traveler_id),
  FOREIGN KEY (admin_id) REFERENCES Admin(admin_id)
);

CREATE TABLE TourOperator
(
  operator_id INT IDENTITY(1,1) NOT NULL,
  company_name VARCHAR(100) NOT NULL,
  email VARCHAR(100) NOT NULL,
  password VARCHAR(100) NOT NULL,
  registration_date DATE NOT NULL,
  is_approved INT NOT NULL,
  last_login_date_ DATE NOT NULL,
  phone VARCHAR(20) NOT NULL,
  admin_id INT NOT NULL,
  PRIMARY KEY (operator_id),
  FOREIGN KEY (admin_id) REFERENCES Admin(admin_id)
);

CREATE TABLE Trip
(
  trip_id INT IDENTITY(101,1) NOT NULL,
  title VARCHAR(100) NOT NULL,
  description TEXT NOT NULL,
  destination VARCHAR(100) NOT NULL,
  price_per_person INT NOT NULL,
  capacity INT NOT NULL,
  start_date DATE NOT NULL,
  end_date DATE NOT NULL,
  sustainability_score INT NOT NULL,
  trip_type VARCHAR(50) NOT NULL,
  duration INT NOT NULL,
  min_group_size INT NOT NULL,
  max_group_size INT NOT NULL,
  views_count INT NOT NULL,
  conversion_rate INT NOT NULL,
  min_budget INT NOT NULL,
  max_budget INT NOT NULL,
  physical_intensity_level INT NOT NULL,
  -- Removed redundant 'available_languages' column
  category_id INT NOT NULL,
  operator_id INT NOT NULL,
  PRIMARY KEY (trip_id),
  FOREIGN KEY (category_id) REFERENCES TripCategory(category_id),
  FOREIGN KEY (operator_id) REFERENCES TourOperator(operator_id)
);

CREATE TABLE Booking
(
  booking_id INT IDENTITY(1,1) NOT NULL,
  booking_date DATE DEFAULT CAST(GETDATE() AS DATE) NOT NULL,
  status VARCHAR(50) NOT NULL,
  total_amount INT NOT NULL,
  cancellation_policy TEXT,
  Group_Size INT NOT NULL,
  Booking_status INT DEFAULT 1 NOT NULL,
  cancellation_date DATE,
  cancellation_reason TEXT,
  booking_timestamp DATE DEFAULT GETDATE() NOT NULL,
  pass_id INT NOT NULL,
  payment_id INT  NOT NULL,
  trip_id INT NOT NULL,
  traveler_id INT NOT NULL,
  PRIMARY KEY (booking_id),
  FOREIGN KEY (pass_id) REFERENCES DigitalTravelPass(pass_id),
  FOREIGN KEY (payment_id) REFERENCES Payment(payment_id),
  FOREIGN KEY (trip_id) REFERENCES Trip(trip_id),
  FOREIGN KEY (traveler_id) REFERENCES Traveler(traveler_id)
);




CREATE TABLE Review
(
  review_id INT IDENTITY(1,1) NOT NULL,
  rating INT NOT NULL,
  comment TEXT NOT NULL,
  review_date DATE NOT NULL,
  moderation_status VARCHAR(50) NOT NULL,
  moderation_date DATE NOT NULL,
  moderation_notes TEXT NOT NULL,
  is_featured INT NOT NULL,
  has_photos INT NOT NULL,
  reported_count INT NOT NULL,
  traveler_id INT NOT NULL,
  trip_id INT NOT NULL,
  admin_id INT NOT NULL,
  PRIMARY KEY (review_id),
  FOREIGN KEY (traveler_id) REFERENCES Traveler(traveler_id),
  FOREIGN KEY (trip_id) REFERENCES Trip(trip_id),
  FOREIGN KEY (admin_id) REFERENCES Admin(admin_id)
);

CREATE TABLE Wishlist
(
  wishlist_entry_id INT IDENTITY(1,1) NOT NULL,
  date_added DATE NOT NULL,
  notes TEXT NOT NULL,
  trip_id INT NOT NULL,
  traveler_id INT NOT NULL,
  PRIMARY KEY (wishlist_entry_id),
  FOREIGN KEY (trip_id) REFERENCES Trip(trip_id),
  FOREIGN KEY (traveler_id) REFERENCES Traveler(traveler_id)
);

CREATE TABLE Assigns
(
  operator_id INT NOT NULL,
  provider_id INT NOT NULL,
  PRIMARY KEY (operator_id, provider_id),
  FOREIGN KEY (operator_id) REFERENCES TourOperator(operator_id),
  FOREIGN KEY (provider_id) REFERENCES ServiceProvider(provider_id)
);

CREATE TABLE is_assigned
(
  trip_id INT NOT NULL,
  assignment_id INT IDENTITY(1,1) NOT NULL,
  PRIMARY KEY (trip_id, assignment_id),
  FOREIGN KEY (trip_id) REFERENCES Trip(trip_id),
  FOREIGN KEY (assignment_id) REFERENCES ServiceAssignment(assignment_id)
);

CREATE TABLE Traveler_preferences
(
  preferences INT IDENTITY(101,1) NOT NULL,
  preferred_trip_type VARCHAR(50) NOT NULL,
  budget_min INT NOT NULL,
  budget_max INT NOT NULL,
  preferred_group_size INT NOT NULL,
  accessibility_needs INT NOT NULL,
  dietary_restrictions INT NOT NULL,
  sustainability_preference INT NOT NULL,
  traveler_id INT NOT NULL,
  PRIMARY KEY (traveler_id),
  FOREIGN KEY (traveler_id) REFERENCES Traveler(traveler_id)
);

CREATE TABLE Trip_Meals
(
  meal_id INT IDENTITY(1,1) NOT NULL,
  meal_type VARCHAR(50) NOT NULL,
  meal_description TEXT NOT NULL,
  dietary_options TEXT NOT NULL,
  included_in_price INT NOT NULL,
  venue VARCHAR(100) NOT NULL,
  trip_id INT NOT NULL,
  PRIMARY KEY (meal_id),
  FOREIGN KEY (trip_id) REFERENCES Trip(trip_id)
);

CREATE TABLE BookingServices_
(
  service_status VARCHAR(50) NOT NULL,
  booking_service_id_ INT IDENTITY(1,1) NOT NULL,
  provider_id INT NOT NULL,
  booking_id INT NOT NULL,
  PRIMARY KEY (booking_service_id_),
  FOREIGN KEY (provider_id) REFERENCES ServiceProvider(provider_id),
  FOREIGN KEY (booking_id) REFERENCES Booking(booking_id)
);

CREATE TABLE AbandonedBooking
(
  abandonment_stage VARCHAR(50) NOT NULL,
  abandonment_reason TEXT NOT NULL,
  abandonment_timestamp DATE NOT NULL,
  potential_revenue INT NOT NULL,
  recovery_attempt_made INT NOT NULL,
  recovered INT NOT NULL,
  abandoned_id INT NOT NULL,
  booking_id INT NOT NULL,
  PRIMARY KEY (abandoned_id),
  FOREIGN KEY (booking_id) REFERENCES Booking(booking_id)
);

CREATE TABLE TripItinerary
(
  itinerary_id INT IDENTITY(1,1) NOT NULL,
  day_number INT NOT NULL,
  location VARCHAR(100) NOT NULL,
  activity_description TEXT NOT NULL,
  start_time DATE NOT NULL,
  end_time DATE NOT NULL,
  accommodation_details TEXT NOT NULL,
  trip_id INT NOT NULL,
  PRIMARY KEY (itinerary_id),
  FOREIGN KEY (trip_id) REFERENCES Trip(trip_id)
);

CREATE TABLE TripAccessibilityFeatures_
(
  feature_id INT IDENTITY(1,1) NOT NULL,
  feature_type VARCHAR(50) NOT NULL,
  feature_description TEXT NOT NULL,
  trip_id INT NOT NULL,
  PRIMARY KEY (feature_id),
  FOREIGN KEY (trip_id) REFERENCES Trip(trip_id)
);

CREATE TABLE TripTags_
(
  tag_id INT IDENTITY(1,1) NOT NULL,
  tag_name VARCHAR(50) NOT NULL,
  trip_id INT NOT NULL,
  PRIMARY KEY (tag_id),
  FOREIGN KEY (trip_id) REFERENCES Trip(trip_id)
);

CREATE TABLE UserActivity_
(
  activity_id INT IDENTITY(1,1) NOT NULL,
  user_id INT NOT NULL,
  user_type VARCHAR(50) NOT NULL,
  activity_type VARCHAR(50) NOT NULL,
  timestamp DATE NOT NULL,
  page_viewed VARCHAR(100) NOT NULL,
  session_id VARCHAR(100) NOT NULL,
  traveler_id INT NOT NULL,
  operator_id INT NOT NULL,
  PRIMARY KEY (activity_id),
  FOREIGN KEY (traveler_id) REFERENCES Traveler(traveler_id),
  FOREIGN KEY (operator_id) REFERENCES TourOperator(operator_id)
);

CREATE TABLE OperatorInquiry_
(
  inquiry_id INT IDENTITY(1,1) NOT NULL,
  inquiry_text TEXT NOT NULL,
  inquiry_timestamp DATETIME NOT NULL,
  response_text TEXT NOT NULL,
  response_timestamp DATETIME NOT NULL,
  is_resolved INT NOT NULL,
  traveler_id INT NOT NULL,
  operator_id INT NOT NULL,
  PRIMARY KEY (inquiry_id),
  FOREIGN KEY (traveler_id) REFERENCES Traveler(traveler_id),
  FOREIGN KEY (operator_id) REFERENCES TourOperator(operator_id)
);

CREATE TABLE TripKeywords
(
  keyword_id INT IDENTITY(1,1) NOT NULL,
  keyword VARCHAR(50) NOT NULL,
  trip_id INT NOT NULL,
  PRIMARY KEY (keyword_id),
  FOREIGN KEY (trip_id) REFERENCES Trip(trip_id)
);

CREATE TABLE TripLanguages
(
  -- Now handles multi-language support
  language_id INT IDENTITY(1,1) NOT NULL,
  language VARCHAR(50) NOT NULL,
  trip_id INT NOT NULL,
  PRIMARY KEY (language_id, trip_id),
  FOREIGN KEY (trip_id) REFERENCES Trip(trip_id)
);

CREATE TABLE UserSearchKeywords_
(
  keyword_id INT IDENTITY(1,1) NOT NULL,
  keyword VARCHAR(50) NOT NULL,
  activity_id INT NOT NULL,
  PRIMARY KEY (keyword_id),
  FOREIGN KEY (activity_id) REFERENCES UserActivity_(activity_id)
);

CREATE TABLE TravelerPreferredDestinations
(
  preference_id INT IDENTITY(1,1) NOT NULL,
  destination VARCHAR(50) NOT NULL,
  traveler_id INT NOT NULL,
  PRIMARY KEY (preference_id),
  FOREIGN KEY (traveler_id) REFERENCES Traveler(traveler_id)
);


ALTER TABLE Admin
ADD CONSTRAINT UQ_Admin_Username UNIQUE (username);


ALTER TABLE Payment
ADD CONSTRAINT CK_Payment_Amount_Positive CHECK (amount > 0),
    CONSTRAINT CK_Payment_Status CHECK (payment_status IN ('Pending', 'Completed', 'Failed', 'Refunded')),
    CONSTRAINT CK_Transaction_Status CHECK (transaction_status IN ('Pending', 'Completed', 'Failed'));

ALTER TABLE ServiceProvider
ADD CONSTRAINT CK_ServiceProvider_Phone_Format CHECK (phone LIKE '+%' OR phone LIKE '0%');

ALTER TABLE TransportProvider
ADD CONSTRAINT CK_TransportProvider_FleetSize_Positive CHECK (fleet_size > 0),
    CONSTRAINT CK_TransportProvider_Capacity_Positive CHECK (capacity_per_vehicle > 0);


ALTER TABLE Hotel
ADD CONSTRAINT CK_Hotel_TotalRooms_Positive CHECK (total_rooms > 0),
    CONSTRAINT CK_Hotel_StarRating_Range CHECK (star_rating BETWEEN 1 AND 5);


ALTER TABLE Traveler
ADD CONSTRAINT UQ_Traveler_Email UNIQUE (email),
    CONSTRAINT CK_Traveler_Age_Positive CHECK (age >= 0);


ALTER TABLE TourOperator
ADD CONSTRAINT UQ_TourOperator_Email UNIQUE (email);


ALTER TABLE Trip
ADD CONSTRAINT CK_Trip_Price_Positive CHECK (price_per_person >= 0),
    CONSTRAINT CK_Trip_Capacity_Positive CHECK (capacity > 0),
    CONSTRAINT CK_Trip_Dates CHECK (start_date < end_date),
    CONSTRAINT CK_Trip_Sustainability_Score CHECK (sustainability_score BETWEEN 0 AND 100),
    CONSTRAINT CK_Trip_GroupSize CHECK (min_group_size <= max_group_size AND min_group_size > 0 AND max_group_size > 0),
    CONSTRAINT CK_Trip_Budget CHECK (min_budget <= max_budget AND min_budget >= 0 AND max_budget >= 0),
    CONSTRAINT CK_Trip_PhysicalIntensity CHECK (physical_intensity_level BETWEEN 1 AND 5);


ALTER TABLE Booking
ADD CONSTRAINT CK_Booking_TotalAmount_Positive CHECK (total_amount >= 0),
    CONSTRAINT CK_Booking_GroupSize_Positive CHECK (Group_Size > 0);


ALTER TABLE Review
ADD CONSTRAINT CK_Review_Rating_Range CHECK (rating BETWEEN 1 AND 5);


ALTER TABLE HotelRoomTypes
ADD CONSTRAINT CK_HotelRoomTypes_MaxOccupancy_Positive CHECK (max_occupancy > 0),
    CONSTRAINT CK_HotelRoomTypes_Price_Positive CHECK (price_per_night >= 0);


ALTER TABLE Traveler_preferences
ADD CONSTRAINT CK_TravelerPreferences_Budget CHECK (budget_min <= budget_max AND budget_min >= 0 AND budget_max >= 0),
    CONSTRAINT CK_TravelerPreferences_GroupSize_Positive CHECK (preferred_group_size > 0);


--Hotel/Service

-- Create service assignment log table if it doesn't exist
-- This table tracks all actions taken on service assignments
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ServiceAssignmentLog')
BEGIN
    CREATE TABLE ServiceAssignmentLog (
        log_id INT IDENTITY(1,1) NOT NULL,
        assignment_id INT NOT NULL,
        provider_id INT NOT NULL,
        action VARCHAR(50) NOT NULL,
        notes TEXT NULL,
        timestamp DATETIME NOT NULL,
        PRIMARY KEY (log_id),
        FOREIGN KEY (assignment_id) REFERENCES ServiceAssignment(assignment_id),
        FOREIGN KEY (provider_id) REFERENCES ServiceProvider(provider_id)
    );
END
GO

-- Stored procedure to get all service assignments for a provider
CREATE OR ALTER PROCEDURE sp_GetProviderAssignments
    @ProviderId INT
AS
BEGIN
    SELECT 
        sa.assignment_id, 
        sa.assigned_date, 
        sa.status,
        t.trip_id,
        t.title AS trip_title,
        t.start_date,
        t.end_date,
        t.max_group_size,
        t.trip_type,
        ton.company_name AS operator_name,
        ton.operator_id
    FROM 
        ServiceAssignment sa
        JOIN is_assigned ia ON sa.assignment_id = ia.assignment_id
        JOIN Trip t ON ia.trip_id = t.trip_id
        JOIN TourOperator ton ON t.operator_id = ton.operator_id
        JOIN ProviderAssignment pa ON sa.assignment_id = pa.assignment_id
    WHERE 
        pa.provider_id = @ProviderId
    ORDER BY
        sa.assigned_date DESC;
END
GO

-- Stored procedure to get detailed information about an assignment
CREATE OR ALTER PROCEDURE sp_GetAssignmentDetails
    @AssignmentId INT
AS
BEGIN
    -- Get main assignment info
    SELECT 
        sa.assignment_id, 
        sa.assigned_date, 
        sa.status,
        sa.is_approved,
        t.trip_id,
        t.title,
        t.description,
        t.destination,
        t.price_per_person,
        t.start_date,
        t.end_date,
        t.trip_type,
        t.min_group_size,
        t.max_group_size,
        t.physical_intensity_level,
        ton.company_name AS operator_name,
        ton.operator_id,
        ton.phone AS operator_phone
    FROM 
        ServiceAssignment sa
        JOIN is_assigned ia ON sa.assignment_id = ia.assignment_id
        JOIN Trip t ON ia.trip_id = t.trip_id
        JOIN TourOperator ton ON t.operator_id = ton.operator_id
    WHERE 
        sa.assignment_id = @AssignmentId;
        
    -- Get trip itinerary
    SELECT 
        trip_id,
        itinerary_id,
        day_number,
        location,
        activity_description,
        start_time,
        end_time,
        accommodation_details
    FROM 
        TripItinerary
    WHERE 
        trip_id IN (
            SELECT t.trip_id
            FROM ServiceAssignment sa
            JOIN is_assigned ia ON sa.assignment_id = ia.assignment_id
            JOIN Trip t ON ia.trip_id = t.trip_id
            WHERE sa.assignment_id = @AssignmentId
        )
    ORDER BY
        day_number;
END
GO

-- Stored procedure to update assignment status
CREATE OR ALTER PROCEDURE sp_UpdateAssignmentStatus
    @AssignmentId INT,
    @Status VARCHAR(50),
    @IsApproved BIT,
    @Notes TEXT = NULL,
    @ProviderId INT = NULL
AS
BEGIN
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Update the assignment status
        UPDATE ServiceAssignment
        SET status = @Status,
            is_approved = @IsApproved
        WHERE assignment_id = @AssignmentId;
        
        -- Log the action if provider ID is supplied
        IF @ProviderId IS NOT NULL
        BEGIN
            INSERT INTO ServiceAssignmentLog (
                assignment_id,
                provider_id,
                action,
                notes,
                timestamp
            ) VALUES (
                @AssignmentId,
                @ProviderId,
                @Status,
                @Notes,
                GETDATE()
            );
        END
        
        COMMIT TRANSACTION;
        SELECT 1 AS Success;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT 0 AS Success, ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

-- Stored procedure to create an operator inquiry
CREATE OR ALTER PROCEDURE sp_CreateOperatorInquiry
    @InquiryText TEXT,
    @OperatorId INT,
    @TravelerId INT = NULL
AS
BEGIN
    INSERT INTO OperatorInquiry_ (
        inquiry_text, 
        inquiry_timestamp, 
        response_text, 
        response_timestamp, 
        is_resolved, 
        traveler_id, 
        operator_id
    ) VALUES (
        @InquiryText, 
        GETDATE(), 
        '', 
        NULL, 
        0, 
        @TravelerId, 
        @OperatorId
    );
    
    SELECT SCOPE_IDENTITY() AS InquiryId;
END
GO

-- Trigger to update BookingServices_ status when service assignment status changes
CREATE OR ALTER TRIGGER trg_UpdateBookingServices
ON ServiceAssignment
AFTER UPDATE
AS
BEGIN
    IF UPDATE(status) OR UPDATE(is_approved)
    BEGIN
        -- Get the affected assignment IDs and new statuses
        DECLARE @StatusChanges TABLE (
            assignment_id INT,
            new_status VARCHAR(50),
            is_approved BIT
        );
        
        INSERT INTO @StatusChanges
        SELECT 
            i.assignment_id,
            i.status,
            i.is_approved
        FROM 
            inserted i
            JOIN deleted d ON i.assignment_id = d.assignment_id
        WHERE 
            i.status <> d.status OR i.is_approved <> d.is_approved;
            
        -- Update related booking services
        UPDATE bs
        SET service_status = sc.new_status
        FROM 
            BookingServices_ bs
            JOIN ProviderAssignment pa ON bs.provider_id = pa.provider_id
            JOIN @StatusChanges sc ON pa.assignment_id = sc.assignment_id;
    END
END
GO


BULK INSERT AbandonedBooking
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\abandoned-booking-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT Admin
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\admin-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT Assigns
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\assigns-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT Booking 
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\booking-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');


BULK INSERT BookingServices_
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\booking-services-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT DigitalTravelPass
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\digital-travel-pass-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT Guide_
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\guide-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT GuideLanguages
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\guidelanguages.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT HotelAmenities
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\hotel-amenities-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT Hotel
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\hotel-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT HotelRoomTypes
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\hotel-room-types-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT is_assigned
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\is-assigned-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT OperatorInquiry_
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\operator-inquiry-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT Payment
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\payment-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT ProviderAssignment
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\provider-assignment-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT Review
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\review-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT ServiceAssignment
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\service-assignment-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT ServiceProvider
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\service-provider-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT TourOperator
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\tour-operator-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT TransportProvider
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\transport-provider-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT Traveler
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\traveler-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT Traveler_preferences
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\traveler-preferences-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT TravelerPreferredDestinations
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\traveler-preferred-destinations-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT TripAccessibilityFeatures_
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\trip-accessibility-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT TripCategory
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\trip-category-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT Trip
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\trip-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT TripItinerary
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\trip-itinerary-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT TripKeywords
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\trip-keywords-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT TripLanguages
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\trip-languages-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT Trip_Meals
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\trip-meals-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT TripTags_
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\trip-tags-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT UserActivity_
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\user-activity-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT UserSearchKeywords_
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\user-search-keywords-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

BULK INSERT Wishlist
FROM 'D:\A-23i-0532-A-23i_0062-A-23i-0747\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\A_I230532_TAHA_I230747_RAFIQUE_I230062_RUHAN\DATASET\wishlist-data.csv'
WITH (FORMAT = 'CSV', FIRSTROW = 2, FIELDTERMINATOR = ',', ROWTERMINATOR = '0x0a');

INSERT INTO AbandonedBooking (abandonment_stage, abandonment_reason, abandonment_timestamp, potential_revenue, recovery_attempt_made, recovered, abandoned_id, booking_id)
VALUES
-- Payment stage abandonments
('Payment', 'Credit card declined', '2025-01-03', 349, 1, 0, 51, 7),
('Payment', 'Technical issue with payment gateway', '2025-01-05', 578, 1, 1, 52, 12),
('Payment', 'Customer hesitation on final price', '2025-01-08', 429, 1, 0, 53, 3),
('Payment', 'Customer found better price elsewhere', '2025-01-10', 899, 1, 0, 54, 22),
('Payment', 'Unexpected service fees', '2025-01-15', 675, 1, 1, 55, 9),
('Payment', 'Credit card declined', '2025-01-17', 782, 1, 0, 56, 31),
('Payment', 'Customer requested different payment method unavailable', '2025-01-22', 525, 0, 0, 57, 4),

-- Initial search stage abandonments
('Initial Search', 'No suitable options available', '2025-01-04', 350, 0, 0, 58, 33),
('Initial Search', 'Price too high', '2025-01-11', 680, 1, 0, 59, 18),
('Initial Search', 'Date availability issues', '2025-01-16', 425, 0, 0, 60, 42),
('Initial Search', 'Unclear booking terms', '2025-01-21', 560, 1, 1, 61, 29),
('Initial Search', 'Technical website issue', '2025-01-25', 399, 0, 0, 62, 8),

-- Registration stage abandonments
('Registration', 'Too much information required', '2025-01-07', 745, 1, 0, 63, 17),
('Registration', 'Privacy concerns', '2025-01-14', 520, 1, 0, 64, 36),
('Registration', 'Form submission error', '2025-01-19', 610, 1, 1, 65, 2),
('Registration', 'Account creation issues', '2025-01-24', 490, 0, 0, 66, 49),
('Registration', 'Email verification failed', '2025-01-29', 550, 1, 0, 67, 11),

-- Cart stage abandonments
('Cart Review', 'Changed mind about product', '2025-01-02', 720, 1, 0, 68, 19),
('Cart Review', 'Unexpected delivery timeframe', '2025-01-09', 480, 1, 1, 69, 44),
('Cart Review', 'Required addon services too expensive', '2025-01-13', 830, 1, 0, 70, 6),
('Cart Review', 'Customer indecision', '2025-01-18', 590, 0, 0, 71, 38),
('Cart Review', 'Comparison shopping', '2025-01-23', 410, 1, 1, 72, 14),

-- Customer information stage abandonments
('Customer Information', 'Privacy concerns', '2025-01-06', 670, 1, 0, 73, 27),
('Customer Information', 'Identity verification issues', '2025-01-12', 540, 1, 0, 74, 1),
('Customer Information', 'Form too complex', '2025-01-20', 390, 0, 0, 75, 47),
('Customer Information', 'Technical error submitting form', '2025-01-26', 620, 1, 1, 76, 23),

-- Final confirmation stage abandonments
('Final Confirmation', 'Last minute price comparison', '2025-01-01', 760, 1, 0, 77, 41),
('Final Confirmation', 'Unexpected terms and conditions', '2025-01-27', 510, 1, 1, 78, 15),
('Final Confirmation', 'Changed travel plans', '2025-01-30', 840, 0, 0, 79, 32),
('Final Confirmation', 'Customer found better option', '2025-01-31', 495, 1, 0, 80, 5),

-- Additional abandonments for February
('Payment', 'Connection timeout during payment', '2025-02-02', 635, 1, 1, 81, 26),
('Initial Search', 'Not enough filter options', '2025-02-05', 470, 0, 0, 82, 39),
('Cart Review', 'Mobile site navigation issues', '2025-02-08', 710, 1, 0, 83, 21),
('Registration', 'Social login failure', '2025-02-10', 580, 1, 1, 84, 13),
('Final Confirmation', 'Session timeout', '2025-02-15', 450, 0, 0, 85, 50);
--Main
select * from Admin

SELECT * FROM Booking
SELECT * FROM Traveler