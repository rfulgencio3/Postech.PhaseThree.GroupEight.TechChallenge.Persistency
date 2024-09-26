DO
$$
BEGIN
   IF NOT EXISTS (
      SELECT FROM pg_catalog.pg_roles
      WHERE rolname = 'postgres') THEN
      CREATE ROLE postgres LOGIN SUPERUSER PASSWORD 'Admin*123';
   END IF;
END
$$;
