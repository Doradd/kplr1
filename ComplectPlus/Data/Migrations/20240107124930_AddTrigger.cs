using ComplectPlus.Models;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.ComponentModel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;

#nullable disable

namespace ComplectPlus.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" CREATE TRIGGER UpdateAndCheckStorageQuantity
           ON Issuances
            AFTER INSERT
            AS
            BEGIN
            SET NOCOUNT ON
            UPDATE s
            SET s.Quantity = s.Quantity - i.Quantity
            FROM Storages AS s
            INNER JOIN inserted AS i ON s.ComponentId = i.ComponentId

            IF EXISTS(SELECT*
           FROM inserted AS i
               INNER JOIN Storages AS s ON i.ComponentId = s.ComponentId
              WHERE s.Quantity<i.Quantity)
            BEGIN
            RAISERROR('Error: На складе нет такого количества комплектующих', 16, 1)
            ROLLBACK TRANSACTION
            END
            END");
           migrationBuilder.Sql(@"  CREATE TRIGGER tr_update_storage
            ON Deliveries
            AFTER INSERT    
           as
            BEGIN
           UPDATE s
            SET s.Quantity = s.Quantity + d.Quantity

            From Storages as s
            INNER JOIN Deliveries AS d ON s.ComponentId = d.ComponentId;
            END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql(@"DROP TRIGGER UpdateAndCheckStorageQuantity");
            //migrationBuilder.Sql(@"DROP TRIGGER tr_update_storage");

        }
    }
}
