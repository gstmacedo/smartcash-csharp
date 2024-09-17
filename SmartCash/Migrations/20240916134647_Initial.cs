using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCash.Migrations
{
    /// <inheritdoc />
    public partial class NomeDaMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assinaturas",
                columns: table => new
                {
                    IdAssinatura = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Tipo = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false),
                    Valor = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assinaturas", x => x.IdAssinatura);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    IdEmpresa = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Cnpj = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Ramo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.IdEmpresa);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Documento = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "FluxoCaixas",
                columns: table => new
                {
                    IdFluxo = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Tipo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Valor = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    EmpresaId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    UsuarioId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FluxoCaixas", x => x.IdFluxo);
                    table.ForeignKey(
                        name: "FK_FluxoCaixas_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "IdEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FluxoCaixas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioEmpresas",
                columns: table => new
                {
                    IdUsuarioEmpresa = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EmpresaId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    UsuarioId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioEmpresas", x => x.IdUsuarioEmpresa);
                    table.ForeignKey(
                        name: "FK_UsuarioEmpresas_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "IdEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioEmpresas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assinatura_Nome",
                table: "Assinaturas",
                column: "Tipo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_Nome",
                table: "Empresas",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FluxoCaixas_EmpresaId",
                table: "FluxoCaixas",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_FluxoCaixas_UsuarioId",
                table: "FluxoCaixas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioEmpresas_EmpresaId",
                table: "UsuarioEmpresas",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioEmpresas_UsuarioId",
                table: "UsuarioEmpresas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Documento",
                table: "Usuarios",
                column: "Documento",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assinaturas");

            migrationBuilder.DropTable(
                name: "FluxoCaixas");

            migrationBuilder.DropTable(
                name: "UsuarioEmpresas");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
