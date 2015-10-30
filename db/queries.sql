SELECT * FROM pmsys_db.users;
SELECT * FROM pmsys_db.projects;
SELECT * FROM pmsys_db.activities;
SELECT * FROM pmsys_db.users_has_activities;
SELECT * FROM pmsys_db.projects_has_users;

delete from pmsys_db.users
where user_name = ´chelo´;

INSERT INTO pmsys_db.users_has_activities 
           (users_usr_id, activities_act_id, activities_projects_prj_id) 
            VALUES (1,1,1);

SELECT `auto_increment` FROM INFORMATION_SCHEMA.TABLES
WHERE table_schema ='pmsys_db' AND table_name = 'users';

SELECT * FROM INFORMATION_SCHEMA.TABLES
WHERE table_name = 'pmsys_db.users';


SELECT * FROM pmsys_db.projects ;

INSERT INTO `pmsys_db`.`projects_has_users`
(`projects_prj_id`,
`users_usr_id`,
`role`)
VALUES
(1,
2,
"TESTER");

LEFT JOIN (pmsys_db.activities, pmsys_db.projects_has_users, pmsys_db.users, pmsys_db.users_has_activities)
ON(pmsys_db.projects.prj_id = pmsys_db.activities.projects_prj_id 
AND pmsys_db.projects.prj_id = pmsys_db.projects_has_users.projects_prj_id
AND pmsys_db.projects_has_users.users_usr_id = pmsys_db.users.usr_id
AND pmsys_db.projects.prj_id = pmsys_db.users_has_activities.activities_projects_prj_id
AND pmsys_db.activities.act_id = pmsys_db.users_has_activities.activities_act_id
AND pmsys_db.users.usr_id = pmsys_db.users_has_activities.users_usr_id);


-- users in project
SELECT projects.prj_id, projects.prj_name, projects.prj_description, users.usr_name, 
projects_has_users.prj_usr_role FROM pmsys_db.projects 
LEFT JOIN (pmsys_db.projects_has_users, pmsys_db.users)
ON(pmsys_db.projects.prj_id = pmsys_db.projects_has_users.projects_prj_id
AND pmsys_db.projects_has_users.users_usr_id = pmsys_db.users.usr_id) 
WHERE projects.prj_status=1 AND users.usr_status = 1;

SELECT projects.prj_id, projects.prj_name, projects.prj_description, projects.prj_status,
users.usr_id, users.usr_name, users.usr_privileges, projects_has_users.prj_usr_role, users.usr_status 
FROM pmsys_db.projects 
LEFT JOIN (pmsys_db.projects_has_users, pmsys_db.users)
ON(pmsys_db.projects.prj_id = pmsys_db.projects_has_users.projects_prj_id
AND pmsys_db.projects_has_users.users_usr_id = pmsys_db.users.usr_id) 
WHERE projects.prj_status=1 AND users.usr_status = 1;

-- act in project
SELECT projects.prj_id, projects.prj_name, projects.prj_description, activities.act_name, 
activities.act_description FROM pmsys_db.projects 
LEFT JOIN (pmsys_db.activities)
ON(pmsys_db.projects.prj_id = pmsys_db.activities.projects_prj_id) 
WHERE projects.prj_status=1;

-- act usr
SELECT activities.act_id, activities.act_name, activities.act_description, activities.act_planned_start,
activities.act_planned_finish, activities.act_real_start, activities.act_real_finish,
users.usr_name, users_has_activities.usr_act_comments, users_has_activities.usr_act_progress
FROM pmsys_db.activities 
LEFT JOIN (pmsys_db.users_has_activities, pmsys_db.users)
ON(pmsys_db.activities.act_id = pmsys_db.users_has_activities.activities_act_id
AND pmsys_db.users_has_activities.users_usr_id = pmsys_db.users.usr_id)
WHERE activities.projects_prj_id = 1;


SELECT prj_id, prj_name, prj_description FROM pmsys_db.projects LEFT JOIN (pmsys_db.activities)
ON(pmsys_db.projects.prj_id = pmsys_db.activities.projects_prj_id)
 WHERE prj_status = 1;
 
 
 
 
 