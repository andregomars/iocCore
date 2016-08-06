--Core_Function

--copy from Core_Function_Bak

set identity_insert dbo.Core_Function on
truncate table Core_Function
insert into Core_Function
(ID
,MenuID
,FunctionName
,FunctionDescription
,FunctionType
,Priority
,InDate
,InUser
,EditDate
,EditUser
)
select 
ID
,MenuID
,FunctionName
,FunctionDescription
,FunctionType
,Priority
,InDate
,InUser
,EditDate
,EditUser
from Core_Function_Bak

set identity_insert dbo.Core_Function off


/*
insert into Core_Function
(MenuID
,FunctionName
,FunctionDescription
,FunctionType
,Priority
,InDate
,InUser)
select 0,'switch_themes','switch_themes','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'edit_themes','edit_themes','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'activate_plugins','activate_plugins','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'edit_plugins','edit_plugins','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'edit_users','edit_users','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'edit_files','edit_files','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'manage_options','manage_options','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'moderate_comments','moderate_comments','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'manage_categories','manage_categories','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'manage_links','manage_links','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'upload_files','upload_files','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'import','import','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'unfiltered_html','unfiltered_html','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'edit_posts','edit_posts','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'edit_others_posts','edit_others_posts','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'edit_published_posts','edit_published_posts','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'publish_posts','publish_posts','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'edit_pages','edit_pages','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'read','read','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'level_10','level_10','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'level_9','level_9','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'level_8','level_8','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'level_7','level_7','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'level_6','level_6','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'level_5','level_5','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'level_4','level_4','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'level_3','level_3','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'level_2','level_2','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'level_1','level_1','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'level_0','level_0','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'edit_others_pages','edit_others_pages','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'edit_published_pages','edit_published_pages','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'publish_pages','publish_pages','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'delete_pages','delete_pages','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'delete_others_pages','delete_others_pages','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'delete_published_pages','delete_published_pages','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'delete_posts','delete_posts','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'delete_others_posts','delete_others_posts','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'delete_published_posts','delete_published_posts','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'delete_private_posts','delete_private_posts','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'edit_private_posts','edit_private_posts','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'read_private_posts','read_private_posts','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'delete_private_pages','delete_private_pages','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'edit_private_pages','edit_private_pages','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'read_private_pages','read_private_pages','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'delete_users','delete_users','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'create_users','create_users','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'unfiltered_upload','unfiltered_upload','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'edit_dashboard','edit_dashboard','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'update_plugins','update_plugins','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'delete_plugins','delete_plugins','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'install_plugins','install_plugins','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'update_themes','update_themes','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'install_themes','install_themes','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'update_core','update_core','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'list_users','list_users','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'remove_users','remove_users','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'add_users','add_users','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'promote_users','promote_users','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'edit_theme_options','edit_theme_options','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'delete_themes','delete_themes','WebFront',0,getdate(),'andre'
UNION ALL
select 0,'export','export','WebFront',0,getdate(),'andre'
*/
