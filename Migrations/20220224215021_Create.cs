﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SomeCatIDK.PirateJim.Migrations
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuildTimeoutChannels",
                columns: table => new
                {
                    ChannelId = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Time = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildTimeoutChannels", x => x.ChannelId);
                });

            migrationBuilder.CreateTable(
                name: "UserTimeouts",
                columns: table => new
                {
                    ChannelId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    UserId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTimeouts", x => new { x.ChannelId, x.UserId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuildTimeoutChannels");

            migrationBuilder.DropTable(
                name: "UserTimeouts");
        }
    }
}
