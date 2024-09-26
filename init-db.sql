DO
$$
BEGIN
   IF NOT EXISTS (
      SELECT FROM pg_catalog.pg_roles
      WHERE rolname = 'admin') THEN
      CREATE ROLE admin LOGIN PASSWORD 'P@ssW0rd2024!Safe';

   END IF;
END
$$;
