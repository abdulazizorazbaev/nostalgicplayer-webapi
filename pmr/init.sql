create table users (
	id bigint generated always as identity primary key,
	first_name varchar(50) not null,
	last_name varchar(50) not null,
	phone_number varchar(13) not null,
	phone_number_confirmed bool default false,
	birth_date date,
	is_male bool not null,
	password_hash text not null,
	salt text not null,
	identity_role text not null,
	image_path text not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table singers (
	id bigint generated always as identity primary key,
	first_name varchar(50) not null,
	last_name varchar(50) not null,
	image_path text not null,
	bio text not null,
	facebook_acc text,
	instagram_acc text,
	youtube_link text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table genres (
	id bigint generated always as identity primary key,
	genre_name varchar(50) not null,
	image_path text not null,
	description text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table musics (
	id bigint generated always as identity primary key,
	genre_id bigint references genres (id),
	singer_id bigint references singers (id),
	music_name text not null,
	duration varchar(5) not null,
	image_path text not null,
	mp3_path text not null,
	description text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table plays (
	id bigint generated always as identity primary key,
	music_id bigint references musics (id),
	created_at timestamp without time zone default now()
);

create table favorites (
	id bigint generated always as identity primary key,
	music_id bigint references musics (id),
	description text,
	created_at timestamp without time zone default now()
);

create table downloads (
	id bigint generated always as identity primary key,
	music_id bigint references musics (id),
	user_id bigint references users (id),
	created_at timestamp without time zone default now()
);

create table albums (
	id bigint generated always as identity primary key,
	music_id bigint references musics (id),
	singer_id bigint references singers (id),
	album_name varchar(50) not null,
	year integer not null,
	image_path text not null,
	description text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);