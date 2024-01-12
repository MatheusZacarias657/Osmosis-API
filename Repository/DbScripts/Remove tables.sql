
DECLARE @nomeTabela AS nvarchar ( 100 ) 

DECLARE tabela_cursor CURSOR FOR 
	SELECT name FROM sys.tables OPEN tabela_cursor FETCH NEXT 
	FROM tabela_cursor INTO @nomeTabela

WHILE @@FETCH_STATUS = 0 
BEGIN
	-- APAGA AS PKs
	DECLARE @stmt VARCHAR(300);
	  
	DECLARE cur CURSOR FOR
		SELECT 'ALTER TABLE ' + OBJECT_SCHEMA_NAME(parent_object_id) + '.' + OBJECT_NAME(parent_object_id) + ' DROP CONSTRAINT [' + name + ']'
		FROM sys.foreign_keys 
		WHERE 
			OBJECT_SCHEMA_NAME(referenced_object_id) = 'dbo' AND 
			OBJECT_NAME(referenced_object_id) = @nomeTabela;
	   
		OPEN cur;
		FETCH cur INTO @stmt;
			WHILE @@FETCH_STATUS = 0
			BEGIN
				--SELECT @nomeTabela,@stmt
				EXEC (@stmt);
				FETCH cur INTO @stmt;
			END
		CLOSE cur;
		DEALLOCATE cur;

	-- APAGA A TABELA
	exec('DROP TABLE ' + @nomeTabela)
	FETCH NEXT FROM tabela_cursor
	INTO @nomeTabela
END
CLOSE tabela_cursor
DEALLOCATE tabela_cursor

--SELECT * 
--FROM sys.foreign_keys
--WHERE referenced_object_id = object_id('dbo.Rules')
