﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postech.PhaseThree.GroupEight.TechChallenge.Persistency.Infra.Migrations
{
    /// <inheritdoc />
    public partial class IX_tb_contact_phone_contact_phone_number : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_tb_contact_phone_contact_phone_number",
                schema: "contacts",
                table: "tb_contact_phone",
                column: "contact_phone_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_tb_contact_phone_contact_phone_number",
                schema: "contacts",
                table: "tb_contact_phone");
        }
    }
}