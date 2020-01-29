
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/11/2017 12:27:13
-- Generated from EDMX file: C:\TransShip\transship\TransShipModel\TransshipDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [transvia];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_shipments_vendors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shipments] DROP CONSTRAINT [FK_shipments_vendors];
GO
IF OBJECT_ID(N'[dbo].[FK_Refcountries22]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customers] DROP CONSTRAINT [FK_Refcountries22];
GO
IF OBJECT_ID(N'[dbo].[FK_Refcountries23]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[state] DROP CONSTRAINT [FK_Refcountries23];
GO
IF OBJECT_ID(N'[dbo].[FK_Refcountries24]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[recurring_addresses] DROP CONSTRAINT [FK_Refcountries24];
GO
IF OBJECT_ID(N'[dbo].[FK_Refcountries27]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[rates] DROP CONSTRAINT [FK_Refcountries27];
GO
IF OBJECT_ID(N'[dbo].[FK_Refcountries30]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shipments] DROP CONSTRAINT [FK_Refcountries30];
GO
IF OBJECT_ID(N'[dbo].[FK_Refcustomers1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[users] DROP CONSTRAINT [FK_Refcustomers1];
GO
IF OBJECT_ID(N'[dbo].[FK_Refcustomers2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customer_contacts] DROP CONSTRAINT [FK_Refcustomers2];
GO
IF OBJECT_ID(N'[dbo].[FK_Refcustomers28]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shipments] DROP CONSTRAINT [FK_Refcustomers28];
GO
IF OBJECT_ID(N'[dbo].[FK_Refcustomers5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[membership_history] DROP CONSTRAINT [FK_Refcustomers5];
GO
IF OBJECT_ID(N'[dbo].[FK_Refcustomers6]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[recurring_addresses] DROP CONSTRAINT [FK_Refcustomers6];
GO
IF OBJECT_ID(N'[dbo].[FK_Reffile_types14]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shipment_attachment] DROP CONSTRAINT [FK_Reffile_types14];
GO
IF OBJECT_ID(N'[dbo].[FK_Refmeasure_units21]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[rates] DROP CONSTRAINT [FK_Refmeasure_units21];
GO
IF OBJECT_ID(N'[dbo].[FK_Refmeasure_units31]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shipment_cost] DROP CONSTRAINT [FK_Refmeasure_units31];
GO
IF OBJECT_ID(N'[dbo].[FK_Refmeasure_units9]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shipment_services] DROP CONSTRAINT [FK_Refmeasure_units9];
GO
IF OBJECT_ID(N'[dbo].[FK_RefMemberships_levels3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[customers] DROP CONSTRAINT [FK_RefMemberships_levels3];
GO
IF OBJECT_ID(N'[dbo].[FK_RefMemberships_levels4]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[membership_history] DROP CONSTRAINT [FK_RefMemberships_levels4];
GO
IF OBJECT_ID(N'[dbo].[FK_Refservice_types10]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[measure_units] DROP CONSTRAINT [FK_Refservice_types10];
GO
IF OBJECT_ID(N'[dbo].[FK_Refservice_types29]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shipments] DROP CONSTRAINT [FK_Refservice_types29];
GO
IF OBJECT_ID(N'[dbo].[FK_Refshipments13]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shipment_attachment] DROP CONSTRAINT [FK_Refshipments13];
GO
IF OBJECT_ID(N'[dbo].[FK_Refshipments15]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shipment_tracking] DROP CONSTRAINT [FK_Refshipments15];
GO
IF OBJECT_ID(N'[dbo].[FK_Refshipments18]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shipment_notes] DROP CONSTRAINT [FK_Refshipments18];
GO
IF OBJECT_ID(N'[dbo].[FK_Refshipments32]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shipment_cost] DROP CONSTRAINT [FK_Refshipments32];
GO
IF OBJECT_ID(N'[dbo].[FK_Refshipments7]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shipment_services] DROP CONSTRAINT [FK_Refshipments7];
GO
IF OBJECT_ID(N'[dbo].[FK_Refusers11]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shipments] DROP CONSTRAINT [FK_Refusers11];
GO
IF OBJECT_ID(N'[dbo].[FK_Refusers12]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shipment_attachment] DROP CONSTRAINT [FK_Refusers12];
GO
IF OBJECT_ID(N'[dbo].[FK_Refusers16]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shipment_tracking] DROP CONSTRAINT [FK_Refusers16];
GO
IF OBJECT_ID(N'[dbo].[FK_Refusers17]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[shipment_notes] DROP CONSTRAINT [FK_Refusers17];
GO
IF OBJECT_ID(N'[dbo].[FK_Refusers33]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[password_revovery] DROP CONSTRAINT [FK_Refusers33];
GO
IF OBJECT_ID(N'[dbo].[FK_Refvendors25]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[rates] DROP CONSTRAINT [FK_Refvendors25];
GO
IF OBJECT_ID(N'[dbo].[FK_Refvendors26]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[vendor_contacts] DROP CONSTRAINT [FK_Refvendors26];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[countries];
GO
IF OBJECT_ID(N'[dbo].[customer_contacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customer_contacts];
GO
IF OBJECT_ID(N'[dbo].[customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[customers];
GO
IF OBJECT_ID(N'[dbo].[file_types]', 'U') IS NOT NULL
    DROP TABLE [dbo].[file_types];
GO
IF OBJECT_ID(N'[dbo].[measure_units]', 'U') IS NOT NULL
    DROP TABLE [dbo].[measure_units];
GO
IF OBJECT_ID(N'[dbo].[membership_history]', 'U') IS NOT NULL
    DROP TABLE [dbo].[membership_history];
GO
IF OBJECT_ID(N'[dbo].[Memberships_levels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Memberships_levels];
GO
IF OBJECT_ID(N'[dbo].[password_revovery]', 'U') IS NOT NULL
    DROP TABLE [dbo].[password_revovery];
GO
IF OBJECT_ID(N'[dbo].[rates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[rates];
GO
IF OBJECT_ID(N'[dbo].[recurring_addresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[recurring_addresses];
GO
IF OBJECT_ID(N'[dbo].[service_types]', 'U') IS NOT NULL
    DROP TABLE [dbo].[service_types];
GO
IF OBJECT_ID(N'[dbo].[shipment_attachment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[shipment_attachment];
GO
IF OBJECT_ID(N'[dbo].[shipment_cost]', 'U') IS NOT NULL
    DROP TABLE [dbo].[shipment_cost];
GO
IF OBJECT_ID(N'[dbo].[shipment_notes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[shipment_notes];
GO
IF OBJECT_ID(N'[dbo].[shipment_services]', 'U') IS NOT NULL
    DROP TABLE [dbo].[shipment_services];
GO
IF OBJECT_ID(N'[dbo].[shipment_tracking]', 'U') IS NOT NULL
    DROP TABLE [dbo].[shipment_tracking];
GO
IF OBJECT_ID(N'[dbo].[shipments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[shipments];
GO
IF OBJECT_ID(N'[dbo].[state]', 'U') IS NOT NULL
    DROP TABLE [dbo].[state];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[users];
GO
IF OBJECT_ID(N'[dbo].[vendor_contacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[vendor_contacts];
GO
IF OBJECT_ID(N'[dbo].[vendors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[vendors];
GO
IF OBJECT_ID(N'[transshipModelStoreContainer].[Customers_view]', 'U') IS NOT NULL
    DROP TABLE [transshipModelStoreContainer].[Customers_view];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'countries'
CREATE TABLE [dbo].[countries] (
    [CountryID] bigint IDENTITY(1,1) NOT NULL,
    [ISO2] char(2)  NULL,
    [CountryName] varchar(80)  NOT NULL,
    [LongCountryName] varchar(80)  NOT NULL,
    [ISO3] char(3)  NULL,
    [NumCode] varchar(6)  NULL,
    [UNMemberState] varchar(12)  NULL,
    [CallingCode] varchar(8)  NULL,
    [CCTLD] varchar(5)  NULL,
    [currency] varchar(20)  NULL,
    [currencySymbol] varchar(5)  NULL,
    [status] varchar(1)  NULL,
    [validate_state] varchar(1)  NULL,
    [mandatory_zipcode] varchar(1)  NULL
);
GO

-- Creating table 'customer_contacts'
CREATE TABLE [dbo].[customer_contacts] (
    [customerId] decimal(24,0)  NOT NULL,
    [contactId] decimal(24,0) IDENTITY(1,1) NOT NULL,
    [full_name] varchar(50)  NOT NULL,
    [title] varchar(25)  NOT NULL,
    [phone_number] varchar(20)  NULL,
    [email] varchar(50)  NOT NULL
);
GO

-- Creating table 'customers'
CREATE TABLE [dbo].[customers] (
    [customerId] decimal(24,0) IDENTITY(1,1) NOT NULL,
    [estimated_membership] decimal(24,0)  NULL,
    [countryId] bigint  NOT NULL,
    [company_name] varchar(100)  NOT NULL,
    [state] varchar(50)  NOT NULL,
    [city] varchar(50)  NULL,
    [address1] varchar(250)  NOT NULL,
    [address2] varchar(250)  NULL,
    [zipcode] varchar(10)  NULL,
    [status] varchar(1)  NOT NULL,
    [logo] nvarchar(max)  NULL,
    [card_number] varchar(30)  NULL,
    [expiration_date] datetime  NULL,
    [register_date] datetime  NULL
);
GO

-- Creating table 'file_types'
CREATE TABLE [dbo].[file_types] (
    [file_typeId] decimal(24,0) IDENTITY(1,1) NOT NULL,
    [description] varchar(50)  NOT NULL,
    [status] varchar(1)  NOT NULL,
    [type] varchar(20)  NULL
);
GO

-- Creating table 'measure_units'
CREATE TABLE [dbo].[measure_units] (
    [service_typeId] decimal(24,0)  NOT NULL,
    [measure_unitId] decimal(24,0) IDENTITY(1,1) NOT NULL,
    [description] varchar(50)  NOT NULL,
    [status] varchar(1)  NOT NULL
);
GO

-- Creating table 'membership_history'
CREATE TABLE [dbo].[membership_history] (
    [membershipId] decimal(24,0)  NOT NULL,
    [customerId] decimal(24,0)  NOT NULL,
    [card_number] varchar(30)  NULL,
    [expiration_date] datetime  NULL,
    [payment_date] datetime  NOT NULL,
    [initial_date] datetime  NOT NULL,
    [final_date] datetime  NOT NULL,
    [membership_HistoryId] decimal(24,0) IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Memberships_levels'
CREATE TABLE [dbo].[Memberships_levels] (
    [membershipId] decimal(24,0) IDENTITY(1,1) NOT NULL,
    [description] varchar(250)  NOT NULL,
    [status] varchar(1)  NOT NULL,
    [price] decimal(10,2)  NOT NULL,
    [max_number_shipments] decimal(10,0)  NOT NULL,
    [price_markup] decimal(10,2)  NOT NULL
);
GO

-- Creating table 'password_revovery'
CREATE TABLE [dbo].[password_revovery] (
    [userId] decimal(24,0)  NOT NULL,
    [random_code] char(10)  NULL,
    [expiration] char(10)  NULL
);
GO

-- Creating table 'rates'
CREATE TABLE [dbo].[rates] (
    [vendorId] decimal(24,0)  NOT NULL,
    [countryId] bigint  NOT NULL,
    [service_typeId] decimal(24,0)  NOT NULL,
    [measure_unitId] decimal(24,0)  NOT NULL,
    [state] varchar(50)  NOT NULL,
    [price] decimal(10,2)  NOT NULL,
    [status] varchar(1)  NOT NULL
);
GO

-- Creating table 'recurring_addresses'
CREATE TABLE [dbo].[recurring_addresses] (
    [addressId] decimal(24,0)  NOT NULL,
    [customerId] decimal(24,0)  NOT NULL,
    [countryId] bigint  NULL,
    [consignee_name] varchar(100)  NOT NULL,
    [target_company] varchar(50)  NULL,
    [phone_number] varchar(50)  NOT NULL,
    [state] varchar(50)  NOT NULL,
    [city] varchar(50)  NULL,
    [address1] varchar(250)  NOT NULL,
    [address2] varchar(250)  NULL,
    [zipcode] varchar(10)  NULL
);
GO

-- Creating table 'service_types'
CREATE TABLE [dbo].[service_types] (
    [service_typeId] decimal(24,0) IDENTITY(1,1) NOT NULL,
    [descripcion] varchar(50)  NOT NULL,
    [user_input] varchar(1)  NOT NULL,
    [status] varchar(1)  NOT NULL
);
GO

-- Creating table 'shipment_attachment'
CREATE TABLE [dbo].[shipment_attachment] (
    [shipmentId] decimal(24,0)  NOT NULL,
    [file_typeId] decimal(24,0)  NOT NULL,
    [shipment_attachmentId] decimal(24,0) IDENTITY(1,1) NOT NULL,
    [userId] decimal(24,0)  NOT NULL,
    [description] varchar(50)  NOT NULL,
    [creation_date] datetime  NOT NULL,
    [file_name] varchar(50)  NULL
);
GO

-- Creating table 'shipment_cost'
CREATE TABLE [dbo].[shipment_cost] (
    [shipmentId] decimal(24,0)  NOT NULL,
    [service_typeId] decimal(24,0)  NOT NULL,
    [measure_unitId] decimal(24,0)  NOT NULL,
    [quantity] decimal(10,0)  NOT NULL,
    [unit_price] decimal(10,2)  NULL
);
GO

-- Creating table 'shipment_notes'
CREATE TABLE [dbo].[shipment_notes] (
    [shipmentId] decimal(24,0)  NOT NULL,
    [shipment_noteId] decimal(24,0)  NOT NULL,
    [userId] decimal(24,0)  NOT NULL,
    [note] varchar(4000)  NOT NULL,
    [status] char(1)  NOT NULL,
    [creation_date] datetime  NOT NULL
);
GO

-- Creating table 'shipment_services'
CREATE TABLE [dbo].[shipment_services] (
    [shipmentId] decimal(24,0)  NOT NULL,
    [service_typeId] decimal(24,0)  NOT NULL,
    [measure_unitId] decimal(24,0)  NOT NULL,
    [quantity] decimal(10,0)  NOT NULL,
    [estimated_unit_price] decimal(10,2)  NOT NULL,
    [confirmed_unit_price] decimal(10,2)  NULL
);
GO

-- Creating table 'shipment_tracking'
CREATE TABLE [dbo].[shipment_tracking] (
    [shipmentId] decimal(24,0)  NOT NULL,
    [trackingId] decimal(24,0) IDENTITY(1,1) NOT NULL,
    [userId] decimal(24,0)  NOT NULL,
    [status] varchar(50)  NOT NULL,
    [comentaries] varchar(1000)  NULL,
    [creation_date] datetime  NOT NULL
);
GO

-- Creating table 'shipments'
CREATE TABLE [dbo].[shipments] (
    [shipmentId] decimal(24,0)  NOT NULL,
    [customerId] decimal(24,0)  NOT NULL,
    [userId] decimal(24,0)  NOT NULL,
    [countryId] bigint  NOT NULL,
    [service_typeId] decimal(24,0)  NOT NULL,
    [consignee_name] varchar(100)  NOT NULL,
    [target_company] varchar(50)  NULL,
    [phone_number] varchar(50)  NOT NULL,
    [state] varchar(50)  NOT NULL,
    [city] varchar(50)  NULL,
    [address1] varchar(250)  NOT NULL,
    [address2] varchar(250)  NULL,
    [zipcode] varchar(10)  NULL,
    [shipping_terms] varchar(5)  NULL,
    [tnc_accepted] varchar(1)  NOT NULL,
    [shipment_date] datetime  NOT NULL,
    [preferred_vendor] varchar(100)  NULL,
    [estimated_delivery_date] datetime  NULL,
    [description] varchar(4000)  NOT NULL,
    [current_tracking] varchar(50)  NULL,
    [current_tracking_date] datetime  NULL,
    [status] varchar(1)  NOT NULL,
    [vendorId] decimal(24,0)  NULL
);
GO

-- Creating table 'state'
CREATE TABLE [dbo].[state] (
    [countryId] bigint  NOT NULL,
    [stateId] varchar(10)  NOT NULL,
    [name] varchar(50)  NOT NULL
);
GO

-- Creating table 'users'
CREATE TABLE [dbo].[users] (
    [userId] decimal(24,0) IDENTITY(1,1) NOT NULL,
    [customerId] decimal(24,0)  NULL,
    [full_name] varchar(250)  NOT NULL,
    [email] varchar(50)  NOT NULL,
    [password] varchar(1000)  NOT NULL,
    [user_type] varchar(1)  NOT NULL,
    [status] varchar(1)  NOT NULL,
    [role] varchar(1)  NOT NULL,
    [confirmationKey] varchar(1000)  NULL
);
GO

-- Creating table 'vendor_contacts'
CREATE TABLE [dbo].[vendor_contacts] (
    [vendorId] decimal(24,0)  NOT NULL,
    [contactId] decimal(24,0) IDENTITY(1,1) NOT NULL,
    [full_name] varchar(50)  NOT NULL,
    [title] varchar(25)  NOT NULL,
    [phone_number] varchar(20)  NULL,
    [email] varchar(50)  NOT NULL
);
GO

-- Creating table 'vendors'
CREATE TABLE [dbo].[vendors] (
    [vendorId] decimal(24,0) IDENTITY(1,1) NOT NULL,
    [countryId] decimal(24,0)  NOT NULL,
    [company_name] varchar(100)  NOT NULL,
    [state] varchar(50)  NOT NULL,
    [city] varchar(50)  NULL,
    [address1] varchar(250)  NOT NULL,
    [address2] varchar(250)  NULL,
    [zipcode] varchar(10)  NULL,
    [status] varchar(1)  NOT NULL,
    [logo] nvarchar(max)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Customers_view'
CREATE TABLE [dbo].[Customers_view] (
    [customerid] decimal(24,0)  NOT NULL,
    [logo] nvarchar(max)  NULL,
    [company_name] varchar(100)  NOT NULL,
    [countryId] bigint  NOT NULL,
    [CountryName] varchar(80)  NULL,
    [state] varchar(50)  NOT NULL,
    [city] varchar(50)  NULL,
    [address1] varchar(250)  NOT NULL,
    [address2] varchar(250)  NULL,
    [zipcode] varchar(10)  NULL,
    [customer_status] varchar(1)  NOT NULL,
    [full_name] varchar(50)  NULL,
    [membershipId] decimal(24,0)  NULL,
    [description] varchar(250)  NULL,
    [expiration_date] datetime  NULL,
    [status] varchar(1)  NULL,
    [final_date] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CountryID] in table 'countries'
ALTER TABLE [dbo].[countries]
ADD CONSTRAINT [PK_countries]
    PRIMARY KEY CLUSTERED ([CountryID] ASC);
GO

-- Creating primary key on [customerId], [contactId] in table 'customer_contacts'
ALTER TABLE [dbo].[customer_contacts]
ADD CONSTRAINT [PK_customer_contacts]
    PRIMARY KEY CLUSTERED ([customerId], [contactId] ASC);
GO

-- Creating primary key on [customerId] in table 'customers'
ALTER TABLE [dbo].[customers]
ADD CONSTRAINT [PK_customers]
    PRIMARY KEY CLUSTERED ([customerId] ASC);
GO

-- Creating primary key on [file_typeId] in table 'file_types'
ALTER TABLE [dbo].[file_types]
ADD CONSTRAINT [PK_file_types]
    PRIMARY KEY CLUSTERED ([file_typeId] ASC);
GO

-- Creating primary key on [service_typeId], [measure_unitId] in table 'measure_units'
ALTER TABLE [dbo].[measure_units]
ADD CONSTRAINT [PK_measure_units]
    PRIMARY KEY CLUSTERED ([service_typeId], [measure_unitId] ASC);
GO

-- Creating primary key on [membership_HistoryId] in table 'membership_history'
ALTER TABLE [dbo].[membership_history]
ADD CONSTRAINT [PK_membership_history]
    PRIMARY KEY CLUSTERED ([membership_HistoryId] ASC);
GO

-- Creating primary key on [membershipId] in table 'Memberships_levels'
ALTER TABLE [dbo].[Memberships_levels]
ADD CONSTRAINT [PK_Memberships_levels]
    PRIMARY KEY CLUSTERED ([membershipId] ASC);
GO

-- Creating primary key on [userId] in table 'password_revovery'
ALTER TABLE [dbo].[password_revovery]
ADD CONSTRAINT [PK_password_revovery]
    PRIMARY KEY CLUSTERED ([userId] ASC);
GO

-- Creating primary key on [vendorId], [countryId], [service_typeId], [measure_unitId], [state] in table 'rates'
ALTER TABLE [dbo].[rates]
ADD CONSTRAINT [PK_rates]
    PRIMARY KEY CLUSTERED ([vendorId], [countryId], [service_typeId], [measure_unitId], [state] ASC);
GO

-- Creating primary key on [customerId], [addressId] in table 'recurring_addresses'
ALTER TABLE [dbo].[recurring_addresses]
ADD CONSTRAINT [PK_recurring_addresses]
    PRIMARY KEY CLUSTERED ([customerId], [addressId] ASC);
GO

-- Creating primary key on [service_typeId] in table 'service_types'
ALTER TABLE [dbo].[service_types]
ADD CONSTRAINT [PK_service_types]
    PRIMARY KEY CLUSTERED ([service_typeId] ASC);
GO

-- Creating primary key on [shipmentId], [file_typeId], [shipment_attachmentId] in table 'shipment_attachment'
ALTER TABLE [dbo].[shipment_attachment]
ADD CONSTRAINT [PK_shipment_attachment]
    PRIMARY KEY CLUSTERED ([shipmentId], [file_typeId], [shipment_attachmentId] ASC);
GO

-- Creating primary key on [shipmentId], [service_typeId], [measure_unitId] in table 'shipment_cost'
ALTER TABLE [dbo].[shipment_cost]
ADD CONSTRAINT [PK_shipment_cost]
    PRIMARY KEY CLUSTERED ([shipmentId], [service_typeId], [measure_unitId] ASC);
GO

-- Creating primary key on [shipmentId], [shipment_noteId] in table 'shipment_notes'
ALTER TABLE [dbo].[shipment_notes]
ADD CONSTRAINT [PK_shipment_notes]
    PRIMARY KEY CLUSTERED ([shipmentId], [shipment_noteId] ASC);
GO

-- Creating primary key on [shipmentId], [service_typeId], [measure_unitId] in table 'shipment_services'
ALTER TABLE [dbo].[shipment_services]
ADD CONSTRAINT [PK_shipment_services]
    PRIMARY KEY CLUSTERED ([shipmentId], [service_typeId], [measure_unitId] ASC);
GO

-- Creating primary key on [shipmentId], [trackingId] in table 'shipment_tracking'
ALTER TABLE [dbo].[shipment_tracking]
ADD CONSTRAINT [PK_shipment_tracking]
    PRIMARY KEY CLUSTERED ([shipmentId], [trackingId] ASC);
GO

-- Creating primary key on [shipmentId] in table 'shipments'
ALTER TABLE [dbo].[shipments]
ADD CONSTRAINT [PK_shipments]
    PRIMARY KEY CLUSTERED ([shipmentId] ASC);
GO

-- Creating primary key on [countryId], [stateId] in table 'state'
ALTER TABLE [dbo].[state]
ADD CONSTRAINT [PK_state]
    PRIMARY KEY CLUSTERED ([countryId], [stateId] ASC);
GO

-- Creating primary key on [userId] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [PK_users]
    PRIMARY KEY CLUSTERED ([userId] ASC);
GO

-- Creating primary key on [vendorId], [contactId] in table 'vendor_contacts'
ALTER TABLE [dbo].[vendor_contacts]
ADD CONSTRAINT [PK_vendor_contacts]
    PRIMARY KEY CLUSTERED ([vendorId], [contactId] ASC);
GO

-- Creating primary key on [vendorId] in table 'vendors'
ALTER TABLE [dbo].[vendors]
ADD CONSTRAINT [PK_vendors]
    PRIMARY KEY CLUSTERED ([vendorId] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [customerid], [company_name], [countryId], [state], [address1], [customer_status] in table 'Customers_view'
ALTER TABLE [dbo].[Customers_view]
ADD CONSTRAINT [PK_Customers_view]
    PRIMARY KEY CLUSTERED ([customerid], [company_name], [countryId], [state], [address1], [customer_status] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [countryId] in table 'customers'
ALTER TABLE [dbo].[customers]
ADD CONSTRAINT [FK_Refcountries22]
    FOREIGN KEY ([countryId])
    REFERENCES [dbo].[countries]
        ([CountryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Refcountries22'
CREATE INDEX [IX_FK_Refcountries22]
ON [dbo].[customers]
    ([countryId]);
GO

-- Creating foreign key on [countryId] in table 'state'
ALTER TABLE [dbo].[state]
ADD CONSTRAINT [FK_Refcountries23]
    FOREIGN KEY ([countryId])
    REFERENCES [dbo].[countries]
        ([CountryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [countryId] in table 'recurring_addresses'
ALTER TABLE [dbo].[recurring_addresses]
ADD CONSTRAINT [FK_Refcountries24]
    FOREIGN KEY ([countryId])
    REFERENCES [dbo].[countries]
        ([CountryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Refcountries24'
CREATE INDEX [IX_FK_Refcountries24]
ON [dbo].[recurring_addresses]
    ([countryId]);
GO

-- Creating foreign key on [countryId] in table 'rates'
ALTER TABLE [dbo].[rates]
ADD CONSTRAINT [FK_Refcountries27]
    FOREIGN KEY ([countryId])
    REFERENCES [dbo].[countries]
        ([CountryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Refcountries27'
CREATE INDEX [IX_FK_Refcountries27]
ON [dbo].[rates]
    ([countryId]);
GO

-- Creating foreign key on [countryId] in table 'shipments'
ALTER TABLE [dbo].[shipments]
ADD CONSTRAINT [FK_Refcountries30]
    FOREIGN KEY ([countryId])
    REFERENCES [dbo].[countries]
        ([CountryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Refcountries30'
CREATE INDEX [IX_FK_Refcountries30]
ON [dbo].[shipments]
    ([countryId]);
GO

-- Creating foreign key on [customerId] in table 'customer_contacts'
ALTER TABLE [dbo].[customer_contacts]
ADD CONSTRAINT [FK_Refcustomers2]
    FOREIGN KEY ([customerId])
    REFERENCES [dbo].[customers]
        ([customerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [customerId] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [FK_Refcustomers1]
    FOREIGN KEY ([customerId])
    REFERENCES [dbo].[customers]
        ([customerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Refcustomers1'
CREATE INDEX [IX_FK_Refcustomers1]
ON [dbo].[users]
    ([customerId]);
GO

-- Creating foreign key on [customerId] in table 'shipments'
ALTER TABLE [dbo].[shipments]
ADD CONSTRAINT [FK_Refcustomers28]
    FOREIGN KEY ([customerId])
    REFERENCES [dbo].[customers]
        ([customerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Refcustomers28'
CREATE INDEX [IX_FK_Refcustomers28]
ON [dbo].[shipments]
    ([customerId]);
GO

-- Creating foreign key on [customerId] in table 'membership_history'
ALTER TABLE [dbo].[membership_history]
ADD CONSTRAINT [FK_Refcustomers5]
    FOREIGN KEY ([customerId])
    REFERENCES [dbo].[customers]
        ([customerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Refcustomers5'
CREATE INDEX [IX_FK_Refcustomers5]
ON [dbo].[membership_history]
    ([customerId]);
GO

-- Creating foreign key on [customerId] in table 'recurring_addresses'
ALTER TABLE [dbo].[recurring_addresses]
ADD CONSTRAINT [FK_Refcustomers6]
    FOREIGN KEY ([customerId])
    REFERENCES [dbo].[customers]
        ([customerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [estimated_membership] in table 'customers'
ALTER TABLE [dbo].[customers]
ADD CONSTRAINT [FK_RefMemberships_levels3]
    FOREIGN KEY ([estimated_membership])
    REFERENCES [dbo].[Memberships_levels]
        ([membershipId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RefMemberships_levels3'
CREATE INDEX [IX_FK_RefMemberships_levels3]
ON [dbo].[customers]
    ([estimated_membership]);
GO

-- Creating foreign key on [file_typeId] in table 'shipment_attachment'
ALTER TABLE [dbo].[shipment_attachment]
ADD CONSTRAINT [FK_Reffile_types14]
    FOREIGN KEY ([file_typeId])
    REFERENCES [dbo].[file_types]
        ([file_typeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Reffile_types14'
CREATE INDEX [IX_FK_Reffile_types14]
ON [dbo].[shipment_attachment]
    ([file_typeId]);
GO

-- Creating foreign key on [service_typeId], [measure_unitId] in table 'rates'
ALTER TABLE [dbo].[rates]
ADD CONSTRAINT [FK_Refmeasure_units21]
    FOREIGN KEY ([service_typeId], [measure_unitId])
    REFERENCES [dbo].[measure_units]
        ([service_typeId], [measure_unitId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Refmeasure_units21'
CREATE INDEX [IX_FK_Refmeasure_units21]
ON [dbo].[rates]
    ([service_typeId], [measure_unitId]);
GO

-- Creating foreign key on [service_typeId], [measure_unitId] in table 'shipment_cost'
ALTER TABLE [dbo].[shipment_cost]
ADD CONSTRAINT [FK_Refmeasure_units31]
    FOREIGN KEY ([service_typeId], [measure_unitId])
    REFERENCES [dbo].[measure_units]
        ([service_typeId], [measure_unitId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Refmeasure_units31'
CREATE INDEX [IX_FK_Refmeasure_units31]
ON [dbo].[shipment_cost]
    ([service_typeId], [measure_unitId]);
GO

-- Creating foreign key on [service_typeId], [measure_unitId] in table 'shipment_services'
ALTER TABLE [dbo].[shipment_services]
ADD CONSTRAINT [FK_Refmeasure_units9]
    FOREIGN KEY ([service_typeId], [measure_unitId])
    REFERENCES [dbo].[measure_units]
        ([service_typeId], [measure_unitId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Refmeasure_units9'
CREATE INDEX [IX_FK_Refmeasure_units9]
ON [dbo].[shipment_services]
    ([service_typeId], [measure_unitId]);
GO

-- Creating foreign key on [service_typeId] in table 'measure_units'
ALTER TABLE [dbo].[measure_units]
ADD CONSTRAINT [FK_Refservice_types10]
    FOREIGN KEY ([service_typeId])
    REFERENCES [dbo].[service_types]
        ([service_typeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [membershipId] in table 'membership_history'
ALTER TABLE [dbo].[membership_history]
ADD CONSTRAINT [FK_RefMemberships_levels4]
    FOREIGN KEY ([membershipId])
    REFERENCES [dbo].[Memberships_levels]
        ([membershipId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RefMemberships_levels4'
CREATE INDEX [IX_FK_RefMemberships_levels4]
ON [dbo].[membership_history]
    ([membershipId]);
GO

-- Creating foreign key on [userId] in table 'password_revovery'
ALTER TABLE [dbo].[password_revovery]
ADD CONSTRAINT [FK_Refusers33]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[users]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [vendorId] in table 'rates'
ALTER TABLE [dbo].[rates]
ADD CONSTRAINT [FK_Refvendors25]
    FOREIGN KEY ([vendorId])
    REFERENCES [dbo].[vendors]
        ([vendorId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [service_typeId] in table 'shipments'
ALTER TABLE [dbo].[shipments]
ADD CONSTRAINT [FK_Refservice_types29]
    FOREIGN KEY ([service_typeId])
    REFERENCES [dbo].[service_types]
        ([service_typeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Refservice_types29'
CREATE INDEX [IX_FK_Refservice_types29]
ON [dbo].[shipments]
    ([service_typeId]);
GO

-- Creating foreign key on [shipmentId] in table 'shipment_attachment'
ALTER TABLE [dbo].[shipment_attachment]
ADD CONSTRAINT [FK_Refshipments13]
    FOREIGN KEY ([shipmentId])
    REFERENCES [dbo].[shipments]
        ([shipmentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [userId] in table 'shipment_attachment'
ALTER TABLE [dbo].[shipment_attachment]
ADD CONSTRAINT [FK_Refusers12]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[users]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Refusers12'
CREATE INDEX [IX_FK_Refusers12]
ON [dbo].[shipment_attachment]
    ([userId]);
GO

-- Creating foreign key on [shipmentId] in table 'shipment_cost'
ALTER TABLE [dbo].[shipment_cost]
ADD CONSTRAINT [FK_Refshipments32]
    FOREIGN KEY ([shipmentId])
    REFERENCES [dbo].[shipments]
        ([shipmentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [shipmentId] in table 'shipment_notes'
ALTER TABLE [dbo].[shipment_notes]
ADD CONSTRAINT [FK_Refshipments18]
    FOREIGN KEY ([shipmentId])
    REFERENCES [dbo].[shipments]
        ([shipmentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [userId] in table 'shipment_notes'
ALTER TABLE [dbo].[shipment_notes]
ADD CONSTRAINT [FK_Refusers17]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[users]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Refusers17'
CREATE INDEX [IX_FK_Refusers17]
ON [dbo].[shipment_notes]
    ([userId]);
GO

-- Creating foreign key on [shipmentId] in table 'shipment_services'
ALTER TABLE [dbo].[shipment_services]
ADD CONSTRAINT [FK_Refshipments7]
    FOREIGN KEY ([shipmentId])
    REFERENCES [dbo].[shipments]
        ([shipmentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [shipmentId] in table 'shipment_tracking'
ALTER TABLE [dbo].[shipment_tracking]
ADD CONSTRAINT [FK_Refshipments15]
    FOREIGN KEY ([shipmentId])
    REFERENCES [dbo].[shipments]
        ([shipmentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [userId] in table 'shipment_tracking'
ALTER TABLE [dbo].[shipment_tracking]
ADD CONSTRAINT [FK_Refusers16]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[users]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Refusers16'
CREATE INDEX [IX_FK_Refusers16]
ON [dbo].[shipment_tracking]
    ([userId]);
GO

-- Creating foreign key on [userId] in table 'shipments'
ALTER TABLE [dbo].[shipments]
ADD CONSTRAINT [FK_Refusers11]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[users]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Refusers11'
CREATE INDEX [IX_FK_Refusers11]
ON [dbo].[shipments]
    ([userId]);
GO

-- Creating foreign key on [vendorId] in table 'vendor_contacts'
ALTER TABLE [dbo].[vendor_contacts]
ADD CONSTRAINT [FK_Refvendors26]
    FOREIGN KEY ([vendorId])
    REFERENCES [dbo].[vendors]
        ([vendorId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [vendorId] in table 'shipments'
ALTER TABLE [dbo].[shipments]
ADD CONSTRAINT [FK_shipments_vendors]
    FOREIGN KEY ([vendorId])
    REFERENCES [dbo].[vendors]
        ([vendorId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_shipments_vendors'
CREATE INDEX [IX_FK_shipments_vendors]
ON [dbo].[shipments]
    ([vendorId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------