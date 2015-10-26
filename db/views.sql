CREATE VIEW `project_user` AS
   SELECT projects.prj_id, projects.prj_name, projects.prj_description, projects.prj_status,
users.usr_id, users.usr_name, users.usr_privileges, projects_has_users.prj_usr_role, users.usr_status 
FROM pmsys_db.projects 
LEFT JOIN (pmsys_db.projects_has_users, pmsys_db.users)
ON(pmsys_db.projects.prj_id = pmsys_db.projects_has_users.projects_prj_id
AND pmsys_db.projects_has_users.users_usr_id = pmsys_db.users.usr_id);
