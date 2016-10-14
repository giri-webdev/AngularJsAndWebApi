SELECT  c.in_cart_id, p.ProductID, p.ProductName, p.UnitPrice,c.dt_date FROM Products p
INNER JOIN Cart c ON p.ProductID = c.in_product_id
WHERE c.bi_active ='true' and c.vc_user_id = '11aa9003-5994-413b-81d0-d1f06fc13450'

----SHOPPING CART
SELECT * FROM Cart
UPDATE Cart set bi_active = 1 WHERE in_cart_id = 1002
ALTER TABLE CART ADD  vc_user_id nvarchar(256)
ALTER TABLE CART DROP COLUMN vc_user_id

---USERS
SELECT * FROM AspNetUsers

----COMMON
SP_HELP Cart
select * from  sys.tables