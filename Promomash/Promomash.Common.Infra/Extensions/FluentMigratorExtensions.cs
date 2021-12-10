using FluentMigrator.Builders.Alter.Table;
using FluentMigrator.Builders.Create;
using FluentMigrator.Builders.Create.ForeignKey;
using FluentMigrator.Builders.Create.Index;
using FluentMigrator.Builders.Create.Table;
using FluentMigrator.Builders.Delete;
using FluentMigrator.SqlServer;

namespace Promomash.Common.Infra.Extensions
{
    /// <summary>
    /// Extensions for FluentMigrator
    /// </summary>
    public static class FluentMigratorExtensions
    {
        /// <summary>
        /// Create foreign key
        /// </summary>
        /// <param name="create">Expression root</param>
        /// <param name="table">Table name</param>
        /// <param name="column">Column name</param>
        /// <param name="toTable">Foreign table name</param>
        /// <param name="primaryColumn">Primary key column name</param>
        public static ICreateForeignKeyCascadeSyntax ForeignKey(
            this ICreateExpressionRoot create,
            string table,
            string column,
            string toTable,
            string primaryColumn
            )
        {
            return create.ForeignKey(GetForeignKeyName(table, column, toTable))
                .FromTable(table).ForeignColumn(column)
                .ToTable(toTable).PrimaryColumn(primaryColumn);
        }

        /// <summary>
        /// Create index
        /// </summary>
        /// <param name="create">Expression root</param>
        /// <param name="table">Table name</param>
        /// <param name="column">Column name</param>
        public static ICreateIndexColumnOptionsSyntax Index(
            this ICreateExpressionRoot create,
            string table,
            string column
            )
        {
            return create.Index(GetIndexName(table, column)).OnTable(table).OnColumn(column);
        }

        /// <summary>
        /// Create primary key
        /// </summary>
        /// <param name="create">Expression root</param>
        /// <param name="table">Table name</param>
        /// <param name="column">Column name</param>
        /// <param name="isClusteredIndex">Is created index should be clustered or non-clustered</param>
        public static void PrimaryKey(
            this ICreateExpressionRoot create,
            string table,
            string column,
            bool isClusteredIndex
            )
        {
            var primaryKey = create.PrimaryKey(GetPrimaryKeyName(table)).OnTable(table).Column(column);

            if (isClusteredIndex)
            {
                primaryKey.Clustered();
            }
            else
            {
                primaryKey.NonClustered();
            }
        }

        /// <summary>
        /// Set column type to nvarchar(max)
        /// </summary>
        /// <param name="createTableColumnAsTypeSyntax">Expression root</param>
        public static ICreateTableColumnOptionOrWithColumnSyntax AsMaxString(
            this ICreateTableColumnAsTypeSyntax createTableColumnAsTypeSyntax
            )
        {
            return createTableColumnAsTypeSyntax.AsString(int.MaxValue);
        }

        /// <summary>
        /// Set column type to nvarchar(max)
        /// </summary>
        /// <param name="alterTableColumnAsTypeSyntax">Expression root</param>
        public static IAlterTableColumnOptionOrAddColumnOrAlterColumnSyntax AsMaxString(
            this IAlterTableColumnAsTypeSyntax alterTableColumnAsTypeSyntax
            )
        {
            return alterTableColumnAsTypeSyntax.AsString(int.MaxValue);
        }

        /// <summary>
        /// Set column type to binary(max)
        /// </summary>
        /// <param name="createTableColumnAsTypeSyntax">Expression root</param>
        public static ICreateTableColumnOptionOrWithColumnSyntax AsMaxBinary(
            this ICreateTableColumnAsTypeSyntax createTableColumnAsTypeSyntax
            )
        {
            return createTableColumnAsTypeSyntax.AsBinary(int.MaxValue);
        }

        /// <summary>
        /// Delete foreign key
        /// </summary>
        /// <param name="delete">Expression root</param>
        /// <param name="table">Table name</param>
        /// <param name="column">Column name</param>
        /// <param name="toTable">Foreign table name</param>
        public static void ForeignKey(
            this IDeleteExpressionRoot delete,
            string table,
            string column,
            string toTable
            )
        {
            delete.ForeignKey(GetForeignKeyName(table, column, toTable)).OnTable(table);
        }

        /// <summary>
        /// Delete index
        /// </summary>
        /// <param name="delete">Expression root</param>
        /// <param name="table">Table name</param>
        /// <param name="column">Column name</param>
        public static void Index(
            this IDeleteExpressionRoot delete,
            string table,
            string column
            )
        {
            delete.Index(GetIndexName(table, column)).OnTable(table).OnColumn(column);
        }

        private static string GetForeignKeyName(string table, string column, string toTable)
        {
            return $"FK_{table}_{toTable}_{column}";
        }

        private static string GetPrimaryKeyName(string table)
        {
            return $"PK_{table}";
        }

        private static string GetIndexName(string table, string column)
        {
            return $"IX_{table}_{column}";
        }
    }
}
