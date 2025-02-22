SET FOREIGN_KEY_CHECKS=0;
DROP TABLE IF EXISTS `pos_saleinvoice`;
DROP TABLE IF EXISTS `pos_saleinvoicedetail`;
SET FOREIGN_KEY_CHECKS=1;

CREATE DATABASE IF NOT EXISTS lms;
USE lms;

CREATE TABLE `pos_saleinvoice` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ClientId` int DEFAULT NULL,
  `Date` datetime DEFAULT NULL,
  `InvoiceNo` varchar(45) DEFAULT NULL,
  `CustomerName` varchar(255) DEFAULT NULL,
  `Cell` varchar(15) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT CURRENT_TIMESTAMP,
  `CreatedById` int DEFAULT NULL,
  `ModifiedOn` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `ModifiedById` int DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`Id`)
);



CREATE TABLE `pos_saleinvoicedetail` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ClientId` int NOT NULL,
  `SaleInvoiceId` int NOT NULL,
  `ItemCodeId` int DEFAULT NULL,
  `BarcodeId` int DEFAULT NULL,
  `Quantity` int DEFAULT NULL,
  `RetailPriceId` int DEFAULT NULL,
  `Discount` decimal(10,2) DEFAULT NULL,
  `LineTotal` decimal(10,2) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT CURRENT_TIMESTAMP,
  `CreatedById` int DEFAULT NULL,
  `ModifiedOn` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `ModifiedById` int DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  `DiscAmount` double DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `pos_saleinvoicedetail_ibfk_1` (`SaleInvoiceId`),
  CONSTRAINT `pos_saleinvoicedetail_ibfk_1` FOREIGN KEY (`SaleInvoiceId`) REFERENCES `pos_saleinvoice` (`Id`) ON DELETE CASCADE
);


DROP TABLE IF EXISTS `pos_items`;

CREATE TABLE `pos_items` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ClientId` int NOT NULL,
  `ItemCode` varchar(45) DEFAULT NULL,
  `Description` varchar(45) DEFAULT NULL,
  `Barcode` int DEFAULT NULL,
  `BrandName` varchar(45) DEFAULT NULL,
  `Size` varchar(45) DEFAULT NULL,
  `Quantity` int DEFAULT NULL,
  `CostPrice` int DEFAULT NULL,
  `WholeSalePrice` int DEFAULT NULL,
  `RetailPrice` int DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `CreatedById` int DEFAULT NULL,
  `ModifiedOn` datetime DEFAULT NULL,
  `ModifiedById` int DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`Id`,`ClientId`)
);

DELIMITER $$

CREATE PROCEDURE `POS_Manage_Items`(
    IN prm_id INT,
    IN prm_clientId INT,
    IN prm_itemCode VARCHAR(45),
    IN prm_description VARCHAR(45),
    IN prm_barcode VARCHAR(45),
    IN prm_brandName VARCHAR(45),
    IN prm_size VARCHAR(45),
    IN prm_quantity INT,
    IN prm_costPrice INT,
    IN prm_wholeSalePrice INT,
    IN prm_retailPrice INT,
    IN prm_createdOn DATETIME,
    IN prm_createdById INT,
    IN prm_modifiedOn DATETIME,
    IN prm_modifiedById INT,
    IN prm_isActive BIT,
    IN prm_dBoperation VARCHAR(10)
)
BEGIN
    IF prm_dBoperation = 'Insert' THEN
        INSERT INTO pos_items (
            clientid,
            ItemCode,
            Description,
            Barcode,
            BrandName,
            Size,
            Quantity,
            CostPrice,
            WholeSalePrice,
            RetailPrice,
            CreatedOn,
            CreatedById,
            ModifiedOn,
            ModifiedById,
            IsActive
        ) VALUES (
            prm_clientId,
            prm_itemCode,
            prm_description,
            prm_barcode,
            prm_brandName,
            prm_size,
            prm_quantity,
            prm_costPrice,
            prm_wholeSalePrice,
            prm_retailPrice,
            prm_createdOn,
            prm_createdById,
            prm_modifiedOn,
            prm_modifiedById,
            prm_isActive
        );
    END IF;

    IF prm_dBoperation = 'Update' THEN
        UPDATE pos_items
        SET 
        clientid = prm_clientId,
            ItemCode = prm_itemCode,
            Description = prm_description,
            Barcode = prm_barcode,
            BrandName = prm_brandName,
            Size = prm_size,
            Quantity = prm_quantity,
            CostPrice = prm_costPrice,
            WholeSalePrice = prm_wholeSalePrice,
            RetailPrice = prm_retailPrice,
            CreatedOn = prm_createdOn,
            CreatedById = prm_createdById,
            ModifiedOn = prm_modifiedOn,
            ModifiedById = prm_modifiedById,
            IsActive = prm_isActive
        WHERE pos_items.id = prm_id ;
    END IF;

    IF prm_dBoperation = 'Delete' THEN
        DELETE FROM pos_items
        WHERE pos_items.id = prm_id ;
    END IF;

    IF prm_dBoperation = 'Activate' THEN
        UPDATE pos_items
        SET IsActive = '1'
        WHERE pos_items.id = prm_id ;
    END IF;

    IF prm_dBoperation = 'Deactivate' THEN
        UPDATE pos_items
        SET IsActive = '0'
        WHERE pos_items.id = prm_id ;
    END IF;
END$$


CREATE PROCEDURE `POS_Manage_SaleInvoice`(
    IN prm_id INT,
    IN prm_clientId INT,
    IN prm_invoiceNo VARCHAR(45),
    IN prm_customerName VARCHAR(255),
    IN prm_cell VARCHAR(50),
    IN prm_date DATETIME,
    IN prm_createdOn DATETIME,
    IN prm_createdById INT,
    IN prm_modifiedOn DATETIME,
    IN prm_modifiedById INT,
    IN prm_isActive BIT,
    IN prm_dBoperation VARCHAR(50)
)
BEGIN
    IF prm_dBoperation = 'Insert' THEN
        INSERT INTO pos_saleinvoice (
            Id,
            ClientId,
            InvoiceNo,
            CustomerName,
            Cell,
            `Date`,
            CreatedOn,
            CreatedById,
            ModifiedOn,
            ModifiedById,
            IsActive
        ) 
        VALUES (
            prm_id,
            prm_clientId,
            prm_invoiceNo,
            prm_customerName,
            prm_cell,
            prm_date,
            prm_createdOn,
            prm_createdById,
            prm_modifiedOn,
            prm_modifiedById,
            prm_isActive
        );
    ELSEIF prm_dBoperation = 'Update' THEN
        UPDATE pos_saleinvoice
        SET 
            ClientId = prm_clientId,
            InvoiceNo = prm_invoiceNo,
            CustomerName = prm_customerName,
            Cell = prm_cell,
            `Date` = prm_date,
            CreatedOn = prm_createdOn,
            CreatedById = prm_createdById,
            ModifiedOn = prm_modifiedOn,
            ModifiedById = prm_modifiedById,
            IsActive = prm_isActive
        WHERE Id = prm_id ;
    ELSEIF prm_dBoperation = 'Delete' THEN
        DELETE FROM pos_saleinvoice
        WHERE Id = prm_id;
    ELSEIF prm_dBoperation = 'Activate' THEN
        UPDATE pos_saleinvoice
        SET IsActive = 1
        WHERE Id = prm_id ;
    ELSEIF prm_dBoperation = 'DeActivate' THEN
        UPDATE pos_saleinvoice
        SET IsActive = 0
        WHERE Id = prm_id ;
    
    END IF;
END $$

CREATE PROCEDURE `POS_Manage_SaleInvoiceDetail`(
    IN prm_id INT,
    IN prm_clientId INT,
    IN prm_saleInvoiceId INT,
    IN prm_itemCodeId INT,
    IN prm_barcodeId INT,
    IN prm_quantity INT,
    IN prm_discount DOUBLE,
    IN prm_discAmount double,
    IN prm_lineTotal DOUBLE,
    IN prm_createdOn DATETIME,
    IN prm_createdById INT,
    IN prm_modifiedOn DATETIME,
    IN prm_modifiedById INT,
    IN prm_isActive BIT,
    IN prm_retailPriceId INT,      -- renamed parameter for numeric foreign key
    IN prm_dBoperation VARCHAR(10)
)
BEGIN
    /* Insert */
    IF prm_dBoperation = 'Insert' THEN
        INSERT INTO pos_saleinvoicedetail (
            ClientId,
            SaleInvoiceId,
            ItemCodeId,
            BarcodeId,
            Quantity,
            Discount,
            DiscAmount,
            LineTotal,
            CreatedOn,
            CreatedById,
            ModifiedOn,
            ModifiedById,
            IsActive,
            RetailPriceId             -- updated column name
        ) 
        VALUES (
            prm_clientId,
            prm_saleInvoiceId,
            prm_itemCodeId,
            prm_barcodeId,
            prm_quantity,
            prm_discount,
            prm_discAmount,
            prm_lineTotal,
            prm_createdOn,
            prm_createdById,
            prm_modifiedOn,
            prm_modifiedById,
            prm_isActive,
            prm_retailPriceId         -- insert numeric foreign key
        );
    END IF;

    /* Update */
    IF prm_dBoperation = 'Update' THEN
        UPDATE pos_saleinvoicedetail
        SET 
            ClientId = prm_clientId,
            ItemCodeId = prm_itemCodeId,
            BarcodeId = prm_barcodeId,
            Quantity = prm_quantity,
            Discount = prm_discount,
            DiscAmount= prm_discAmount,
            LineTotal = prm_lineTotal,
            CreatedOn = prm_createdOn,
            CreatedById = prm_createdById,
            ModifiedOn = prm_modifiedOn,
            ModifiedById = prm_modifiedById,
            IsActive = prm_isActive,
            RetailPriceId = prm_retailPriceId  -- updated column name
        WHERE 
            Id = prm_id AND 
            SaleInvoiceId = prm_saleInvoiceId;
    END IF;

    /* Delete */
    IF prm_dBoperation = 'Delete' THEN
        DELETE FROM pos_saleinvoicedetail
        WHERE 
            Id = prm_id AND 
            SaleInvoiceId = prm_saleInvoiceId;
    END IF;

    /* Activate */
    IF prm_dBoperation = 'Activate' THEN
        UPDATE pos_saleinvoicedetail
        SET IsActive = 1
        WHERE 
            Id = prm_id AND 
            SaleInvoiceId = prm_saleInvoiceId;
    END IF;

    /* Deactivate */
    IF prm_dBoperation = 'DeActivate' THEN
        UPDATE pos_saleinvoicedetail
        SET IsActive = 0
        WHERE 
            Id = prm_id AND 
            SaleInvoiceId = prm_saleInvoiceId;
    END IF;
END$$



CREATE PROCEDURE `POS_Search_Items`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * FROM Pos_Items ';
    set @querystr= concat(@querystr,whereClause);
PREPARE stmt1 
FROM
@querystr;
 EXECUTE stmt1;
 DEALLOCATE PREPARE stmt1;
END $$

DELIMITER $$
CREATE PROCEDURE `POS_Search_SaleInvoice`(IN whereClause VARCHAR(5000))
BEGIN
    SET @querystr = 'SELECT 
        pos.Id,
        pos.ClientId,
        pos.InvoiceNo,
        pos.CustomerName,
        pos.Cell,
        pos.Date,
        pos.CreatedOn,
        pos.CreatedById,
        pos.ModifiedOn,
        pos.ModifiedById,
        pos.IsActive
    FROM pos_saleinvoice AS pos';

    SET @querystr = CONCAT(@querystr, whereClause);

    PREPARE stmt1 FROM @querystr;
    EXECUTE stmt1;
    DEALLOCATE PREPARE stmt1;
END $$




CREATE PROCEDURE `POS_Search_SaleInvoiceDetail`(IN whereClause VARCHAR(5000))
BEGIN
    SET @querystr = 'SELECT 
        posd.Id,
        posd.ClientId,
        posd.SaleInvoiceId,
        posd.ItemCodeId,       -- numeric foreign key for item code
        posd.BarcodeId,        -- numeric foreign key for barcode
        posd.RetailPriceId,    -- numeric foreign key for retail price
        posd.Quantity,
        posd.Discount,
        posd.DiscAmount,
        posd.LineTotal,
        posd.CreatedOn,
        posd.CreatedById,
        posd.ModifiedOn,
        posd.ModifiedById,
        posd.IsActive,
        barcodeItem.Barcode AS barcode,       -- join for barcode display value
        retailItem.RetailPrice AS retailPrice,  -- join for retail price display value
        itemCodeItem.ItemCode AS itemCode       -- join for item code display value
    FROM pos_saleinvoicedetail AS posd
        LEFT JOIN pos_items AS barcodeItem ON (barcodeItem.Id = posd.BarcodeId)
        LEFT JOIN pos_items AS retailItem ON (retailItem.Id = posd.RetailPriceId)
        LEFT JOIN pos_items AS itemCodeItem ON (itemCodeItem.Id = posd.ItemCodeId)
    ';

    SET @querystr = CONCAT(@querystr, whereClause);

    PREPARE stmt1 FROM @querystr;
    EXECUTE stmt1;
    DEALLOCATE PREPARE stmt1;
END $$

DELIMITER ;

DELIMITER ;;
CREATE PROCEDURE `GetNextPageNumber`(IN clientId INT, 
    IN tableName VARCHAR(50), 
    IN columnName VARCHAR(50), 
    IN prefix VARCHAR(10), 
    OUT nextNo VARCHAR(25))
BEGIN
    DECLARE latestNo VARCHAR(25);
    DECLARE numberPart INT;
    DECLARE newNumberPart INT;
    DECLARE dynamicQuery VARCHAR(255);

    SET @dynamicQuery = CONCAT(
        'SELECT ', columnName, ' INTO @latestNo FROM ', tableName,
        ' WHERE ClientId = ', clientId, 
        ' ORDER BY Id DESC LIMIT 1'
    );


    PREPARE stmt FROM @dynamicQuery;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;

    SET latestNo = @latestNo;

    IF latestNo IS NULL THEN
        SET numberPart = 0;
    ELSE
        SET numberPart = CAST(REPLACE(SUBSTRING(latestNo, LENGTH(prefix) + 1), '-', '') AS UNSIGNED);
    END IF;

    SET newNumberPart = numberPart + 1;
    SET nextNo = CONCAT(prefix, LPAD(newNumberPart, 6, '0'));

END ;;
DELIMITER ;