using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scriptingo.Common
{
    internal class DbİnitScripts
    {
        public string MsSql_CreateViews = @"CREATE VIEW [dbo].[v_primary_keys] AS
                                            SELECT 
                                                Top 1000 KU.table_name as TABLENAME
                                                ,column_name as PRIMARYKEYCOLUMN
                                            FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC 

                                            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU
                                                ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' 
                                                AND TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME 
                                            ORDER BY 
                                                    KU.TABLE_NAME
                                                ,KU.ORDINAL_POSITION
                                            ; 
                                            GO

                                            CREATE VIEW [dbo].[v_columns] AS
                                            SELECT
                                                DB_NAME() AS [Database_Name],
                                                s.name AS [Schema_Name],
                                                t.name AS [Table_Name],
                                                c.name AS [Column_Name],
                                                ty.name AS [Column_Data_Type],
                                                uty.name AS [Column_System_Type],
                                                c.max_length AS [Column_Maximum_Length],
                                                c.precision AS [Column_Precision],
                                                c.scale AS [Column_Scale],
                                                CASE
                                                pk.PRIMARYKEYCOLUMN
                                                when c.name Then 'Yes'
                                                else 'No'
                                                END AS [Column_Has_Primary_Key],
                                                CASE
                                                c.is_nullable
                                                WHEN 1 THEN 'Yes'
                                                WHEN 0 THEN 'No'
                                                END AS [Column_Is_Nullable],
                                                CASE
                                                c.is_identity
                                                WHEN 1 THEN 'Yes'
                                                WHEN 0 THEN 'No'
                                                END AS [Column_Has_Identity],
                                                CASE
                                                c.is_computed
                                                WHEN 1 THEN 'Yes'
                                                WHEN 0 THEN 'No'
                                                END AS [Column_Is_Computed],
                                                cc.definition AS [Computed_Column_Definition],
                                                c.column_id as [Column_Id],
                                                Case
  	                                            When t.name IN (
                                                'sysdiagrams',
                                                '__MigrationHistory',
                                                'process',
                                                'api_doc',
                                                'con_type',
                                                'con'
                                                ) Then
  	                                            1
                                            ELSE
	                                            0
                                            END As Is_System_Table
                                            FROM
                                                sys.tables AS t
                                                INNER JOIN sys.schemas AS s ON t.schema_id = s.schema_id
                                                INNER JOIN sys.columns AS c ON t.object_id = c.object_id
                                                INNER JOIN sys.types AS ty ON ty.user_type_id = c.user_type_id
                                                INNER JOIN sys.types AS uty ON ty.system_type_id = uty.user_type_id
                                                LEFT JOIN sys.computed_columns AS cc ON t.object_id = cc.object_id
                                                AND cc.column_id = c.column_id
                                                LEFT JOIN v_primary_keys AS pk ON t.name = pk.TABLENAME
                                                AND pk.PRIMARYKEYCOLUMN = c.name
                                            UNION ALL
                                            SELECT
                                                DB_NAME() AS DatabaseName,
                                                s.name AS SchemaName,
                                                t.name AS TableName,
                                                c.name AS ColumnName,
                                                ty.name AS ColumnDataType,
                                                ty.name AS ColumnSystemTypeName,
                                                c.max_length AS ColumnMaximumLength,
                                                c.precision AS ColumnPrecision,
                                                c.scale AS ColumnScale,
                                                CASE
                                                c.is_nullable
                                                WHEN 1 THEN 'Yes'
                                                WHEN 0 THEN 'No'
                                                END AS ColumnIsNullable,
                                                CASE
                                                c.is_identity
                                                WHEN 1 THEN 'Yes'
                                                WHEN 0 THEN 'No'
                                                END AS ColumnHasIdentity,
                                                CASE
                                                pk.PRIMARYKEYCOLUMN
                                                when c.name Then 'Yes'
                                                else 'No'
                                                END AS [Column_Has_Primary_Key],
                                                CASE
                                                c.is_computed
                                                WHEN 1 THEN 'Yes'
                                                WHEN 0 THEN 'No'
                                                END AS ColumnIsComputed,
                                                cc.definition AS [Computed Column Definition],
                                                c.column_id as [Column_Id],
                                                Case
  	                                            When t.name IN (
                                                'sysdiagrams',
                                                '__MigrationHistory',
                                                'process',
                                                'api_doc',
                                                'con_type',
                                                'con'
                                                ) Then
  	                                            1
                                            ELSE
	                                            0
                                            END As Is_System_Table
                                            FROM
                                                sys.tables AS t
                                                INNER JOIN sys.schemas AS s ON t.schema_id = s.schema_id
                                                INNER JOIN sys.columns AS c ON t.object_id = c.object_id
                                                INNER JOIN sys.types AS ty ON ty.user_type_id = c.user_type_id
                                                AND ty.is_assembly_type = 1
                                                LEFT JOIN sys.computed_columns AS cc ON t.object_id = cc.object_id
                                                AND cc.column_id = c.column_id
                                                LEFT JOIN v_primary_keys AS pk ON t.name = pk.TABLENAME
                                                AND pk.PRIMARYKEYCOLUMN = c.name;
                                            GO";
    }
}
