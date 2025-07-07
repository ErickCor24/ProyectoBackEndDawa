using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndDawa.Migrations
{
    /// <inheritdoc />
    public partial class add_all_dbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserve_Client_ClientId",
                table: "Reserve");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserve_Vehicle_VehicleId",
                table: "Reserve");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClient_Client_ClientId",
                table: "UserClient");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCompany_Companies_CompanyId",
                table: "UserCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Companies_CompanyId",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCompany",
                table: "UserCompany");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClient",
                table: "UserClient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reserve",
                table: "Reserve");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "Vehicles");

            migrationBuilder.RenameTable(
                name: "UserCompany",
                newName: "UserCompanies");

            migrationBuilder.RenameTable(
                name: "UserClient",
                newName: "UserClients");

            migrationBuilder.RenameTable(
                name: "Reserve",
                newName: "Reserves");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_CompanyId",
                table: "Vehicles",
                newName: "IX_Vehicles_CompanyId");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "UserCompanies",
                newName: "Email");

            migrationBuilder.RenameIndex(
                name: "IX_UserCompany_CompanyId",
                table: "UserCompanies",
                newName: "IX_UserCompanies_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_UserClient_ClientId",
                table: "UserClients",
                newName: "IX_UserClients_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Reserve_VehicleId",
                table: "Reserves",
                newName: "IX_Reserves_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Reserve_ClientId",
                table: "Reserves",
                newName: "IX_Reserves_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCompanies",
                table: "UserCompanies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClients",
                table: "UserClients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reserves",
                table: "Reserves",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserves_Clients_ClientId",
                table: "Reserves",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserves_Vehicles_VehicleId",
                table: "Reserves",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClients_Clients_ClientId",
                table: "UserClients",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompanies_Companies_CompanyId",
                table: "UserCompanies",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Companies_CompanyId",
                table: "Vehicles",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserves_Clients_ClientId",
                table: "Reserves");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserves_Vehicles_VehicleId",
                table: "Reserves");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClients_Clients_ClientId",
                table: "UserClients");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCompanies_Companies_CompanyId",
                table: "UserCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Companies_CompanyId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCompanies",
                table: "UserCompanies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClients",
                table: "UserClients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reserves",
                table: "Reserves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "Vehicle");

            migrationBuilder.RenameTable(
                name: "UserCompanies",
                newName: "UserCompany");

            migrationBuilder.RenameTable(
                name: "UserClients",
                newName: "UserClient");

            migrationBuilder.RenameTable(
                name: "Reserves",
                newName: "Reserve");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_CompanyId",
                table: "Vehicle",
                newName: "IX_Vehicle_CompanyId");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "UserCompany",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_UserCompanies_CompanyId",
                table: "UserCompany",
                newName: "IX_UserCompany_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_UserClients_ClientId",
                table: "UserClient",
                newName: "IX_UserClient_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Reserves_VehicleId",
                table: "Reserve",
                newName: "IX_Reserve_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Reserves_ClientId",
                table: "Reserve",
                newName: "IX_Reserve_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCompany",
                table: "UserCompany",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClient",
                table: "UserClient",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reserve",
                table: "Reserve",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserve_Client_ClientId",
                table: "Reserve",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserve_Vehicle_VehicleId",
                table: "Reserve",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClient_Client_ClientId",
                table: "UserClient",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompany_Companies_CompanyId",
                table: "UserCompany",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Companies_CompanyId",
                table: "Vehicle",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
