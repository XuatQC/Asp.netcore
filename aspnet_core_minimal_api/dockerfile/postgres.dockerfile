FROM postgres:15

LABEL version="1.0.0"
LABEL description="PostgresDB Server"
LABEL maintainer="HungDH <hungdh@saishunkansys.com>"

EXPOSE 5432

RUN apt-get update && apt-get install vim -y

# copy scripts
COPY ../dockerfile/data/sql/* /docker-entrypoint-initdb.d/
# COPY ../data/mysql/* /opt