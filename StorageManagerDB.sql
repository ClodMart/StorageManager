PGDMP     *    0                {           StorageManagerDB    15.2    15.2 K    t           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            u           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            v           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            w           1262    16748    StorageManagerDB    DATABASE     �   CREATE DATABASE "StorageManagerDB" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Italian_Italy.1252';
 "   DROP DATABASE "StorageManagerDB";
                postgres    false            �            1259    16873    CategoryIngredientList    TABLE       CREATE TABLE "public"."CategoryIngredientList" (
    "EntryId" bigint NOT NULL,
    "Category_Id" bigint NOT NULL,
    "Ingredient_id" bigint NOT NULL,
    "Selected" boolean DEFAULT false NOT NULL,
    "Quantity" integer DEFAULT 1 NOT NULL,
    "SelectedFormat_Id" bigint
);
 .   DROP TABLE "public"."CategoryIngredientList";
       public         heap    postgres    false            �            1259    16900 "   CategoryIngredientList_EntryId_seq    SEQUENCE     �   ALTER TABLE "public"."CategoryIngredientList" ALTER COLUMN "EntryId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "public"."CategoryIngredientList_EntryId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    229            �            1259    16780    Ingredients    TABLE     �  CREATE TABLE "public"."Ingredients" (
    "Id" bigint NOT NULL,
    "Name" "text" NOT NULL,
    "Category" "text" NOT NULL,
    "IsUsedValue" bigint NOT NULL,
    "Notes" "text",
    "QuantityNeeded" numeric NOT NULL,
    "ActualQuantity" numeric NOT NULL,
    "IsEnough" boolean GENERATED ALWAYS AS (
CASE
    WHEN ("QuantityNeeded" > "ActualQuantity") THEN false
    ELSE true
END) STORED NOT NULL
);
 #   DROP TABLE "public"."Ingredients";
       public         heap    postgres    false            �            1259    16815    Ingredients_Format    TABLE     �  CREATE TABLE "public"."Ingredients_Format" (
    "Id" bigint NOT NULL,
    "Ingredient_Id" bigint NOT NULL,
    "Supplier_Id" bigint NOT NULL,
    "Cost_€" numeric NOT NULL,
    "PastCost1" numeric DEFAULT 0,
    "PastCost2" numeric,
    "PastCost3" numeric,
    "Size_Kg" numeric NOT NULL,
    "Size_Unit" integer DEFAULT 1 NOT NULL,
    "Cost_€/Kg" numeric GENERATED ALWAYS AS (("Cost_€" / "Size_Kg")) STORED,
    "Cost_€/Unit" numeric GENERATED ALWAYS AS (("Cost_€" / ("Size_Unit")::numeric)) STORED,
    "CostDifference" numeric GENERATED ALWAYS AS (("Cost_€" - "PastCost1")) STORED,
    "LastOrderDate" "date",
    "IsDefault" boolean DEFAULT false NOT NULL,
    "LastPriceChange" "date"
);
 *   DROP TABLE "public"."Ingredients_Format";
       public         heap    postgres    false            �            1259    16814    Ingredients_Format_Id_seq    SEQUENCE     �   ALTER TABLE "public"."Ingredients_Format" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "public"."Ingredients_Format_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    223            �            1259    16779    Ingredients_Id_seq    SEQUENCE     �   ALTER TABLE "public"."Ingredients" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "public"."Ingredients_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    219            �            1259    16755    IsUsedValue    TABLE     �   CREATE TABLE "public"."IsUsedValue" (
    "Id" bigint NOT NULL,
    "Description" "text" NOT NULL,
    "CorrespondsToUsed" boolean DEFAULT false NOT NULL
);
 #   DROP TABLE "public"."IsUsedValue";
       public         heap    postgres    false            �            1259    16770    IsUsedValue_Id_seq    SEQUENCE     �   ALTER TABLE "public"."IsUsedValue" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "public"."IsUsedValue_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    214            �            1259    16866    OrderCategory    TABLE     �   CREATE TABLE "public"."OrderCategory" (
    "Id" bigint NOT NULL,
    "Name" "text" NOT NULL,
    "Description" "text" NOT NULL
);
 %   DROP TABLE "public"."OrderCategory";
       public         heap    postgres    false            �            1259    16893    OrderCategory_Id_seq    SEQUENCE     �   ALTER TABLE "public"."OrderCategory" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "public"."OrderCategory_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    228            �            1259    16839    Orders    TABLE     �   CREATE TABLE "public"."Orders" (
    "Id" bigint NOT NULL,
    "Supplier_Id" bigint NOT NULL,
    "OrderDateTime" timestamp without time zone NOT NULL
);
    DROP TABLE "public"."Orders";
       public         heap    postgres    false            �            1259    16850 
   OrdersList    TABLE     �   CREATE TABLE "public"."OrdersList" (
    "Entry_Id" bigint NOT NULL,
    "Order_Id" bigint NOT NULL,
    "Ingredient_Id" bigint NOT NULL,
    "Quantity" bigint DEFAULT 1 NOT NULL
);
 "   DROP TABLE "public"."OrdersList";
       public         heap    postgres    false            �            1259    16849    OrdersList_Entry_Id_seq    SEQUENCE     �   ALTER TABLE "public"."OrdersList" ALTER COLUMN "Entry_Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "public"."OrdersList_Entry_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    227            �            1259    16838    Orders_Entry_Id_seq    SEQUENCE     �   ALTER TABLE "public"."Orders" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "public"."Orders_Entry_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    225            �            1259    16932    Product_Composition    TABLE     �   CREATE TABLE "public"."Product_Composition" (
    "Id" bigint NOT NULL,
    "Product_Id" bigint NOT NULL,
    "Ingredient_Id" bigint NOT NULL,
    "Quantity" double precision DEFAULT 1 NOT NULL,
    "Cost" double precision
);
 +   DROP TABLE "public"."Product_Composition";
       public         heap    postgres    false            �            1259    16931    Product_Composition_Id_seq    SEQUENCE     �   ALTER TABLE "public"."Product_Composition" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "public"."Product_Composition_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    235            �            1259    16923    Products    TABLE     �   CREATE TABLE "public"."Products" (
    "Id" bigint NOT NULL,
    "Product_Name" "text" NOT NULL,
    "Product_Price" double precision DEFAULT 0 NOT NULL,
    "Product_Cost" double precision,
    "Product_Category" "text" NOT NULL,
    "Notes" "text"
);
     DROP TABLE "public"."Products";
       public         heap    postgres    false            �            1259    16922    Products_Id_seq    SEQUENCE     �   ALTER TABLE "public"."Products" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "public"."Products_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    233            �            1259    16794 	   Suppliers    TABLE     �   CREATE TABLE "public"."Suppliers" (
    "Id" bigint NOT NULL,
    "Supplier_Name" "text" NOT NULL,
    "PT_IVA" "text",
    "Phone" "text",
    "Email" "text",
    "Notes" "text"
);
 !   DROP TABLE "public"."Suppliers";
       public         heap    postgres    false            �            1259    16793    Suppliers_Id_seq    SEQUENCE     �   ALTER TABLE "public"."Suppliers" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "public"."Suppliers_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    221            �            1259    16772    UnitsOfMeasure    TABLE     h   CREATE TABLE "public"."UnitsOfMeasure" (
    "Id" bigint NOT NULL,
    "Description" "text" NOT NULL
);
 &   DROP TABLE "public"."UnitsOfMeasure";
       public         heap    postgres    false            �            1259    16771    UnitsOfMeasure_Id_seq    SEQUENCE     �   ALTER TABLE "public"."UnitsOfMeasure" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "public"."UnitsOfMeasure_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    217            �            1259    33363    Users    TABLE     |   CREATE TABLE "public"."Users" (
    "Id" bigint NOT NULL,
    "Username" "text" NOT NULL,
    "Password" "text" NOT NULL
);
    DROP TABLE "public"."Users";
       public         heap    postgres    false            �            1259    33362    Users_Id_seq    SEQUENCE     �   ALTER TABLE "public"."Users" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME "public"."Users_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    237            i          0    16873    CategoryIngredientList 
   TABLE DATA                 public          postgres    false    229   �[       _          0    16780    Ingredients 
   TABLE DATA                 public          postgres    false    219   �\       c          0    16815    Ingredients_Format 
   TABLE DATA                 public          postgres    false    223   $m       Z          0    16755    IsUsedValue 
   TABLE DATA                 public          postgres    false    214   ��       h          0    16866    OrderCategory 
   TABLE DATA                 public          postgres    false    228   j�       e          0    16839    Orders 
   TABLE DATA                 public          postgres    false    225   �       g          0    16850 
   OrdersList 
   TABLE DATA                 public          postgres    false    227   �       o          0    16932    Product_Composition 
   TABLE DATA                 public          postgres    false    235   ��       m          0    16923    Products 
   TABLE DATA                 public          postgres    false    233   \�       a          0    16794 	   Suppliers 
   TABLE DATA                 public          postgres    false    221   �       ]          0    16772    UnitsOfMeasure 
   TABLE DATA                 public          postgres    false    217   ֆ       q          0    33363    Users 
   TABLE DATA                 public          postgres    false    237   e�       x           0    0 "   CategoryIngredientList_EntryId_seq    SEQUENCE SET     U   SELECT pg_catalog.setval('"public"."CategoryIngredientList_EntryId_seq"', 13, true);
          public          postgres    false    231            y           0    0    Ingredients_Format_Id_seq    SEQUENCE SET     M   SELECT pg_catalog.setval('"public"."Ingredients_Format_Id_seq"', 382, true);
          public          postgres    false    222            z           0    0    Ingredients_Id_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('"public"."Ingredients_Id_seq"', 252, true);
          public          postgres    false    218            {           0    0    IsUsedValue_Id_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('"public"."IsUsedValue_Id_seq"', 8, true);
          public          postgres    false    215            |           0    0    OrderCategory_Id_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('"public"."OrderCategory_Id_seq"', 3, true);
          public          postgres    false    230            }           0    0    OrdersList_Entry_Id_seq    SEQUENCE SET     I   SELECT pg_catalog.setval('"public"."OrdersList_Entry_Id_seq"', 6, true);
          public          postgres    false    226            ~           0    0    Orders_Entry_Id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('"public"."Orders_Entry_Id_seq"', 4, true);
          public          postgres    false    224                       0    0    Product_Composition_Id_seq    SEQUENCE SET     M   SELECT pg_catalog.setval('"public"."Product_Composition_Id_seq"', 13, true);
          public          postgres    false    234            �           0    0    Products_Id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('"public"."Products_Id_seq"', 18, true);
          public          postgres    false    232            �           0    0    Suppliers_Id_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('"public"."Suppliers_Id_seq"', 29, true);
          public          postgres    false    220            �           0    0    UnitsOfMeasure_Id_seq    SEQUENCE SET     G   SELECT pg_catalog.setval('"public"."UnitsOfMeasure_Id_seq"', 6, true);
          public          postgres    false    216            �           0    0    Users_Id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('"public"."Users_Id_seq"', 1, true);
          public          postgres    false    236            �           2606    16826 *   Ingredients_Format Ingredients_Format_pkey 
   CONSTRAINT     p   ALTER TABLE ONLY "public"."Ingredients_Format"
    ADD CONSTRAINT "Ingredients_Format_pkey" PRIMARY KEY ("Id");
 Z   ALTER TABLE ONLY "public"."Ingredients_Format" DROP CONSTRAINT "Ingredients_Format_pkey";
       public            postgres    false    223            �           2606    16787    Ingredients Ingredients_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY "public"."Ingredients"
    ADD CONSTRAINT "Ingredients_pkey" PRIMARY KEY ("Id");
 L   ALTER TABLE ONLY "public"."Ingredients" DROP CONSTRAINT "Ingredients_pkey";
       public            postgres    false    219            �           2606    16763    IsUsedValue IsUsedValue_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY "public"."IsUsedValue"
    ADD CONSTRAINT "IsUsedValue_pkey" PRIMARY KEY ("Id");
 L   ALTER TABLE ONLY "public"."IsUsedValue" DROP CONSTRAINT "IsUsedValue_pkey";
       public            postgres    false    214            �           2606    16872     OrderCategory OrderCategory_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY "public"."OrderCategory"
    ADD CONSTRAINT "OrderCategory_pkey" PRIMARY KEY ("Id");
 P   ALTER TABLE ONLY "public"."OrderCategory" DROP CONSTRAINT "OrderCategory_pkey";
       public            postgres    false    228            �           2606    16877 &   CategoryIngredientList PK_CategoryList 
   CONSTRAINT     q   ALTER TABLE ONLY "public"."CategoryIngredientList"
    ADD CONSTRAINT "PK_CategoryList" PRIMARY KEY ("EntryId");
 V   ALTER TABLE ONLY "public"."CategoryIngredientList" DROP CONSTRAINT "PK_CategoryList";
       public            postgres    false    229            �           2606    16937 ,   Product_Composition Product_Composition_pkey 
   CONSTRAINT     r   ALTER TABLE ONLY "public"."Product_Composition"
    ADD CONSTRAINT "Product_Composition_pkey" PRIMARY KEY ("Id");
 \   ALTER TABLE ONLY "public"."Product_Composition" DROP CONSTRAINT "Product_Composition_pkey";
       public            postgres    false    235            �           2606    16929    Products Products_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY "public"."Products"
    ADD CONSTRAINT "Products_pkey" PRIMARY KEY ("Id");
 F   ALTER TABLE ONLY "public"."Products" DROP CONSTRAINT "Products_pkey";
       public            postgres    false    233            �           2606    16800    Suppliers Suppliers_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY "public"."Suppliers"
    ADD CONSTRAINT "Suppliers_pkey" PRIMARY KEY ("Id");
 H   ALTER TABLE ONLY "public"."Suppliers" DROP CONSTRAINT "Suppliers_pkey";
       public            postgres    false    221            �           2606    16778 "   UnitsOfMeasure UnitsOfMeasure_pkey 
   CONSTRAINT     h   ALTER TABLE ONLY "public"."UnitsOfMeasure"
    ADD CONSTRAINT "UnitsOfMeasure_pkey" PRIMARY KEY ("Id");
 R   ALTER TABLE ONLY "public"."UnitsOfMeasure" DROP CONSTRAINT "UnitsOfMeasure_pkey";
       public            postgres    false    217            �           2606    33369    Users Users_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY "public"."Users"
    ADD CONSTRAINT "Users_pkey" PRIMARY KEY ("Id");
 @   ALTER TABLE ONLY "public"."Users" DROP CONSTRAINT "Users_pkey";
       public            postgres    false    237            �           2606    16855    OrdersList pk_OrderList 
   CONSTRAINT     c   ALTER TABLE ONLY "public"."OrdersList"
    ADD CONSTRAINT "pk_OrderList" PRIMARY KEY ("Entry_Id");
 G   ALTER TABLE ONLY "public"."OrdersList" DROP CONSTRAINT "pk_OrderList";
       public            postgres    false    227            �           2606    16843    Orders pk_Orders 
   CONSTRAINT     V   ALTER TABLE ONLY "public"."Orders"
    ADD CONSTRAINT "pk_Orders" PRIMARY KEY ("Id");
 @   ALTER TABLE ONLY "public"."Orders" DROP CONSTRAINT "pk_Orders";
       public            postgres    false    225            �           2606    16878 "   CategoryIngredientList fk_Category    FK CONSTRAINT     �   ALTER TABLE ONLY "public"."CategoryIngredientList"
    ADD CONSTRAINT "fk_Category" FOREIGN KEY ("Category_Id") REFERENCES "public"."OrderCategory"("Id");
 R   ALTER TABLE ONLY "public"."CategoryIngredientList" DROP CONSTRAINT "fk_Category";
       public          postgres    false    229    3256    228            �           2606    16967     CategoryIngredientList fk_Format    FK CONSTRAINT     �   ALTER TABLE ONLY "public"."CategoryIngredientList"
    ADD CONSTRAINT "fk_Format" FOREIGN KEY ("SelectedFormat_Id") REFERENCES "public"."Ingredients_Format"("Id") NOT VALID;
 P   ALTER TABLE ONLY "public"."CategoryIngredientList" DROP CONSTRAINT "fk_Format";
       public          postgres    false    229    3250    223            �           2606    16888    OrdersList fk_Ingredient    FK CONSTRAINT     �   ALTER TABLE ONLY "public"."OrdersList"
    ADD CONSTRAINT "fk_Ingredient" FOREIGN KEY ("Ingredient_Id") REFERENCES "public"."Ingredients"("Id") NOT VALID;
 H   ALTER TABLE ONLY "public"."OrdersList" DROP CONSTRAINT "fk_Ingredient";
       public          postgres    false    227    219    3246            �           2606    16943 $   Product_Composition fk_Ingredient_Id    FK CONSTRAINT     �   ALTER TABLE ONLY "public"."Product_Composition"
    ADD CONSTRAINT "fk_Ingredient_Id" FOREIGN KEY ("Ingredient_Id") REFERENCES "public"."Ingredients"("Id");
 T   ALTER TABLE ONLY "public"."Product_Composition" DROP CONSTRAINT "fk_Ingredient_Id";
       public          postgres    false    3246    235    219            �           2606    16832 !   Ingredients_Format fk_Ingredients    FK CONSTRAINT     �   ALTER TABLE ONLY "public"."Ingredients_Format"
    ADD CONSTRAINT "fk_Ingredients" FOREIGN KEY ("Ingredient_Id") REFERENCES "public"."Ingredients"("Id");
 Q   ALTER TABLE ONLY "public"."Ingredients_Format" DROP CONSTRAINT "fk_Ingredients";
       public          postgres    false    3246    219    223            �           2606    16883 %   CategoryIngredientList fk_Ingredients    FK CONSTRAINT     �   ALTER TABLE ONLY "public"."CategoryIngredientList"
    ADD CONSTRAINT "fk_Ingredients" FOREIGN KEY ("Ingredient_id") REFERENCES "public"."Ingredients"("Id");
 U   ALTER TABLE ONLY "public"."CategoryIngredientList" DROP CONSTRAINT "fk_Ingredients";
       public          postgres    false    219    3246    229            �           2606    16856    OrdersList fk_Order    FK CONSTRAINT     �   ALTER TABLE ONLY "public"."OrdersList"
    ADD CONSTRAINT "fk_Order" FOREIGN KEY ("Order_Id") REFERENCES "public"."Orders"("Id");
 C   ALTER TABLE ONLY "public"."OrdersList" DROP CONSTRAINT "fk_Order";
       public          postgres    false    225    3252    227            �           2606    16938 !   Product_Composition fk_Product_Id    FK CONSTRAINT     �   ALTER TABLE ONLY "public"."Product_Composition"
    ADD CONSTRAINT "fk_Product_Id" FOREIGN KEY ("Product_Id") REFERENCES "public"."Products"("Id");
 Q   ALTER TABLE ONLY "public"."Product_Composition" DROP CONSTRAINT "fk_Product_Id";
       public          postgres    false    233    235    3260            �           2606    16827    Ingredients_Format fk_Supplier    FK CONSTRAINT     �   ALTER TABLE ONLY "public"."Ingredients_Format"
    ADD CONSTRAINT "fk_Supplier" FOREIGN KEY ("Supplier_Id") REFERENCES "public"."Suppliers"("Id");
 N   ALTER TABLE ONLY "public"."Ingredients_Format" DROP CONSTRAINT "fk_Supplier";
       public          postgres    false    221    223    3248            �           2606    16844    Orders fk_Suppliers    FK CONSTRAINT     �   ALTER TABLE ONLY "public"."Orders"
    ADD CONSTRAINT "fk_Suppliers" FOREIGN KEY ("Supplier_Id") REFERENCES "public"."Suppliers"("Id");
 C   ALTER TABLE ONLY "public"."Orders" DROP CONSTRAINT "fk_Suppliers";
       public          postgres    false    3248    225    221            �           2606    16788    Ingredients fk_UsedValue    FK CONSTRAINT     �   ALTER TABLE ONLY "public"."Ingredients"
    ADD CONSTRAINT "fk_UsedValue" FOREIGN KEY ("IsUsedValue") REFERENCES "public"."IsUsedValue"("Id") ON UPDATE RESTRICT ON DELETE CASCADE;
 H   ALTER TABLE ONLY "public"."Ingredients" DROP CONSTRAINT "fk_UsedValue";
       public          postgres    false    3242    219    214            i     x�Օ�j�0��}�C�Z�a�G�]�͍����®�m�pv�xѷ�����@'���s>"J�W5��^���>���u:�WѝzuԪ3���wft����=����쐪U�f�}h:���������L�����U%�E�
�C��lW�&���e>��R>���FӇrS�GO��`d��`fN}0�`�)�COoĿ�S>�hƏ���	ǃ�-~���~l�'��瘪�6/�GR<
B�|��ď���O� ��pf���U&�� �9`�R����õ�*      _      x��]�n����S4$�E�	DQB�Zd��5��t��]ݜe``~3���1ϓS^!o�'IU5)�"�\S{�ے�\�U_UW}�k�l+t�q��׿�zs��J|��������˕��J��Z����������>}���_�/Ƿ���K��������Ǐ�OϿ����#}Z�y���~���w�<(ku��B��]�*� �N����6Z���L}_��[ն���M��?	���/���[k�`����|Q$��%<���)K�`n�n�!� �Z���F r$b$=�_��Esq 
+�h��`x����g�io�<iel+sU.o�H|8~y�&��Yg�`	���l�K����^d���h8Dq�$bU��q����o���#pR�� e��9�m ~9�����Y����o�ߞ>}<_�?����??}��8o �x|O��~����i���,
^�%��Bfڔ�+�!<X�]�3�Wj&m=�r��{��"�m��#nU-\��{�a!D
�X�2��BDi�I=�m�$���NaA9#�Z��j#���+&ж�����R;��V~ϙ�c�ò�M�Ć�Y����f�a�����j3�׮U�,�����˺,3�Q�*H&��, ��o`1k��T�A��*�M�M�_c�A�
Va�eg�	<�d͕��p<��le8uG�h~k�<�	&���si/��8��W�v��z����XXv �.�������O�T���<>���a�
V���(/]G��N��P��(������I�
+�J��k�e
�"d.L �"�J�t홇S�\GL���d��dj1de��kIE�bo�F/R@�4u���Ɓ͂��>���b�/f�h��*�b
�&��K��*#$x�������E[Ӛ�*��bR%җ�R��%Y{R�?E���
������^�>k��UF�)f�;S��pL�&d�<�y���I��Ek�z���.$\%,`��Z�֥�#:�A�!�����F5� �H�4XUI�վ���)O�&��h�3�5�fq5�Y��v��Gt���n�����^�F��X��q`k�oFg7dD�:��p�FX�?�J\mH�.�E����ᚳ��Fd��ԯ;��e�L�ᣪ��Rň	����UL��Um;����xz+$�5�WY��y�E} b+!X?�V:��#�F�F���,�m����IJx�%4�%ҙF�{/;���*w'������`'�F�.�؋$ց�Q§9 ����5U�Bj<m�:6�ɥlh���&<�Q�K"W�(;�Z�RP��ɍ�kk%_0H�.,������ٝ�z�����(^�L�+7H��,U���3+m���ˁ	,�{�t�3�� ��eaۧ�1��j��n�]#�J��qO�Ux&)1�6�p��,��t(͕�����4�@�
	����d��Y��Y�S�;;��2A�r@?�r�7(ڮ6LJt�NY���lZR���z7�ҝ�4.�,̂�4��"Q�,�mH��a��MF�@o��fI�R�5���ͼv��I>��"EBQ[��0\�4bLpS�p�mÞw�A��3�9_���T��3e��&	���nV��M�L�L���۔ƈp��Ufخ�Vږ������^13t���̽�E1��B|���Ç'�$�|`����_a�ɂ��
|^i�x�r�VR�ب�@��[)�?��O�1bɔ�Ѓ�wn�M'��V:g��ɨO1��ՖI;�v��������`n7����9/'� �&`T�Z?�1[�&`2��0i�/�8H�5��L����pˇF��2?�R6��'TX>���F�z?����A+FB�x�ձ-\��/-����,L������2*��_2A���{�v�e_��=�*� �#�~��	)���n{^�`�}�����Ym�L0�<����� �0Jej�7�
0������G�h �R*�c��BeiO�zS/�kχ�>I���w��R	v\�$�P�hw+_4S���p�0،���Ύ��� 4�<,s�L�>H��^'���`KG����@����ڱq���P�'����'T!)\VHH��Tg| J>b�
�\��ym`�~�;w�!I�|6eF+/X�\\�X��R�r�B�ez�Fe�Uɕ��:%�_}��U?@IAAњ
[�	�,v�sJVCҟ�t�J�GMP$H����^�
��#ң��;I-���V#�H8��˅<.�v�^<	=չ�#UJ�=���y6+�s��ϊ�:�����_�F�*��H�r���C�W�^�eORM�"ӥV�r}����*a�FF^�s^05>�ⴙh�F˼��G��:��Ay>Ѕ�3>���G���x�t/��X�!�n�0�N1a\���s��m��m�3�qA�#�w�,H�|�+^M���%Τ���	ct�;��e��@IҙD��O����b���L��P�eM��mtICs����� ��0��	��a����@1�k�_�q��)�v���sK�߿�
b;�B[�L�u0$�K�� Z^_K�CM�r����)\��̊N�ۢ�:<���K��z�c2����Νi	�s�N|��T-��l}��GNׇrD������r/���CP��3�QRbv��-���p��[��q�����M#C0�
�.��]��lE��h����Z6�n�c�����3�ctq���M§ƓgP��\�	�q�1bT�"�O�uF	�%&N��&���'�p=���%h��!#Pgr�'�%S^AB��+����Ln,W���Aפ�˚�#Ԇ��_Hz�-p |�,O��p�Ƒ(�� �-ԯ7#.�'º	���ÍP��5E9�J�9p���W���Ƿ�\@��z`["���F%]��(��@�7?�$99���[4��bT�&�Io)�k�s�
%���X%ۛ��I�h�ƶހ($����X<7$������:!F���}W�8f�STd�e� '���y	ItQx��T6S��d�؛�A	+`�H�t���Ӳ	��d���6�r��o���E�lQ�>M�Qv pI��u4�|��Ǌ�k��{?NY�L�f��ݍ�x���B�&2�V@��3T����k2���	IL�#i���0HKRZ<`�NIA����5/�kW��$:gٌm��V�4)SD ~f�C����b��0�z���g�lV�I��1>_�FZ�t'6�����&��`*�/n*/;T
����8��o��𼲒�z��_����r��"B����n�~�lR���>���y�\-�$@�m�N�\z�0a�m�˙�g�%I�/A�����:���ۋ��0����{H�s��x�J{I�F�_���DM"FP6�3~Y�,ӊ��ܡ~�V����`u�Es�Ն����o�������1M������/+Qq���@�t������so��nE�&97�e�p�	
�^wJ�nR֓�Ryp��j�d�6
��1IM�r���_���H>��NjXc��+t$l�r�L����la:��4S&L "������u_b�H6on,]/���j�W&��F��l�ζ\5p=r�;h�H�e���M���S#���<u�J�3E���	b6WzD���������5��k`�d�9���@���ՙ����|1"�;�(��,Ӹ�A^���Nሤ�\��-���L־�ɳ���)#᡼�-F�F]ڶ0��X4v�����,�������ư�a&<���iHf����7�蝧��֜��&ɻ�4�a��@k�\�ƣ^jz��E(��*r)���\�m�bS���!]\�G%�YI���r��k��fBF�����b�L��R�W�X�T����^�(� q�>���ub��=vt� �6ls�.�H����VH䎴���e���&�v��� n^�sYQ�v��D�{�������- �͗V�^�V"F��T��;a�7�8
p�� �|��S��� ���ZL��^����k<�̤�?�B��!>$C�6*�\K\��v��e{���n2����<")�q_�N���h�� Y   D�*xE���c�ٲNFhnm��"x���)����/��{�/�;)����^�f3`��V>0��yvxk�ҙE)�{�W, @�|��a�U      c      x��ˮ���<ő'�dJu�.e��@�2�8K�/��<y�<Iz�j�����_�Y�Q�>U��k��ٗ�?��ۻg_~��ݓ�����W?<qO������_����͋?����/�>���ɳ�<���w/�<��￼���~�ۛ�/�����߿~��-���׿$�x��������__��_���G_�����g/�����g�?�|�K��_�~�����x����O>����Ͽ���gϾ����=����}��/��\���ӻ��.<��������l�?���^���^~��<{L�BzzׄFrm��5��V�bX��J.�ר�K@E�"�(��w�Q�16�M3R���ӻ���խ�/��-�Uȍ3��a*}��Vªn2�ڼy5;��Z�a�>(���K�/׸���ɦ��=4���3V���^8a�<_g�#HN^��+�Űh�,L�Q�.�k�pP����܃K�2
`%�jg����Y�O	���
��ԗѫCR	��T�W�d�p�js����Qް�F7m��BqX�e�Ph1�m7EU�ޭf�@H1��}9��#
uj���fv��!;#��F\��HR���_q��VZ,?yS�2��C�N^���]�k��~w�Z;,��3FMU�S����8~R�AB������.�.�EdϸJ���.׸��V�"HrOή���B�EXQ�%�.����]�n87Q�ܗ�[�Y�pf��>�h�&�A�sFj�"U�Y�����3�	���x�#���UG��*ag	��}���K��i=dM+h)��im�*��{��i�UX-��������.�R�U�kk_p<�"��Gs��5)�*!E�5��	�n�bG�oX���(C���.׸6ZL��1������P�<xE#T�vH1)*n���WB��9��bTU��s6��#)�.T�|q^g�V��
�#-��xᮄV��i-�B�bR�r��Yp��H��v����1�q�	��

ɛ��-;[�>Jf��(?y��j������=RLc:���L<�Buܸ����^�0�<��i�?1"���sM|���I5�(�f����a��4�������_
��|���Nx(q�2�����9��{�Ӫ�/�b�����2_���*=]�D2�KJ)Ou���؎�;�VEX��*f�#�p�ʖ��p1{�{Ĝ����]Gm�{���(��킼+��FT���.9#�5+�	�&��K�����ǈ8�%lg�����w����2������m7��-G�=u�����r�,�	3V�eT��-��`_Ym{�H@/g�9QY�k2X1+J�I�u��T�.�WGV��%��+!�2� *�(G�1��&��E���星�U�Ф�E�ԑQ�r���v�8yGRDI/4�۰tK �U 	��Xw�2^�PZT�.����P6�]lp�X�\N�f଄?��T��~Y�4��	φx�MFt��� �YD�eT*(,�[���">Z%��?e�)��"P�I�)��5 ��h�3�E| �����&t!wT�w��J�)��8l׿��R�9�lv��۟�"P�g�r!��XT�I��&n���c0��(�$���p(|��k�TT�R����),�T���C�أ���*]�w��Y$�'��wTS6�	�8�)!���U�᷁��`�T�Ң7�-��(�e�X�����2�-5�Ϛ�@l.i��s�]�����M�VG�^�aoIU;xm�6T=��-H���NXi�|ݿĂ�ն����Y_��
�	�"�#�KA����ۓ$v�k�E�g��Y]����xd�Ӣ~�^�V���OP�@�����To�^�����=�{���-3�<��kD�.�6
Mn���>����欚�
9���S����Q���Ū�S��谀Kq��_�Sp�fm�¢L�\#����C�X�����,�
JB�eU�!�������c�5�M�ۡ^�դ\;pX�� ��]�4�Aކ:��(�,v�(�^PZt��!��+�V��
��R���h|���-�E��K�>qN:�―���
��%����ݝ?LP\�Wt�>/vą�>��Ej��65����h�`G_�nS"����R1VV�׭�$g�c1QF���JVX�A�\����%�P!�p�{���-�E�!ʉv�?��ơp��������zȰ��m�֗ѿC_)/.��*�K��s\�� GoS"�_�t�������eTVP��]C�!���R:<�P{Ձ�� �{�
,õ��\�B�OY%fŷC�A�yB)%|O�d_-°����A�Ŋ]2��_�e���J��pv���bgq'�Ӥ���
�
�]`��e?��f!E�2�TlN�ep������Ћ	��`�c)+�����5����J����T�-��(��0�bKi�x68�#6c��x���M�R�J��˂�)/+��kEvք���q�Êe�"�8�� vu��'�([!�o@��\�,�5�Ś�ޖo�,XMX�^O�T�B+('�q�.3��(<���M��y.>��d�V��̑�T�x�X0P�6M�8�ԌI�RV�2��8����q���),�ǃ>���	.]�#�܍���������BF�p�)L�6%��&�#�&���G�>x�� �<�b�.˹[l�	+�LYv��%Cb���镨��$t��x	/n����b�Ї�p��ċ�T���d�W�9j2 �%�͹�Qc�؝�ǵ�x�~ctԘ�2^Xt�r�иa"&@Kh�;��s�|��c��+(DV`���$T�<�o�!J�y����*��}+�Ky�$6n�;n�
\���{�VI5��\�.��O���/rU��Ԓ.い�^�*����)�05�:/UE��q�9�A�=(ѡ*�>Ǽ�����1T^<2��vۻ�x���Ϫ=Dȣ����0���X��Z�V����RZ,D oSb�n����`ud�-�4���+���S\���v��wI���3�V�ЪS���2h-��U9i���Ør��;-�6U���:���:-���g��[SZ�v)y�\�ȈMXѦ���(t�Ď�p�ॼ(NZE���a]Ƹ<��8i��U��<�Ɏ`�$�?�>�O|���,��M'��l7�	+�>�&,d����SAKh�dX��IK4��:)��kCq�:���ה7��,nS�3'����/]�|4h	-��bcTJ��xq�Q^$J�n�ϑ7,��*ߙ��V�Wo���,<��u��i�;ޣ#��ǖEe5��C��`���w�?��*,.S���������|�O�P���(.���ig5�����H���3'~T/6��˂<����\S3^�=`),:d�$�3�&،�p�Ua5	��4`	�,N��N�'x)���\�%:���2�*�<V`t�[�$���bw��e�œ��mf;��j��G�َ[+5�b\\��j���`[M8�+N��%�7'��������O�$���҅V���.#/�%X�,���gtQ(�"��-����+�RJ�^h�8�z�*���󢺏���m1b��ќ3�� �duPM�`>yb��?�j�U��1�^��QT,�EZ��r}B�c��ƤpI9<߈N畮9��1)X
+˼{�@���e2�nAdTy�%�9O5��^W����jF��Ɋ���	s_��dh	-�Z�hyd(?N�;�B�˴�kH�Q�.ȪNhѶi:��t�X@�OqEjs�A�ݢVf
�v��exWE<�wƊ�	MK�9*Z&/.��B�U��4A�]���ɡ=��)q
u�H;W���	+��V��i���t6V������Z��5�%�:.��)����f��Ңl��s��bw�
��VRZ<����5��A�x���s"g�/@o��j)�^��}��ך̎s��r���w�� Niѳh>�$mf!�&�(`�3�-J��h�}�}6� �ZFqѾ�pz�׍��ce��b_�Ѥ[�����Ed���}tnKIqQ�����cU��4�8~s\|��Z���zX�$���� I  �&�M9}�o5�9aE>�$�������9-zt�7W�f��Ѣ�ŏ��b*�h���}���ޮѱ/�QDխ�"-t�4OM���)p�ʊ�D(zM4��S��+,���XHh�SJ����ސ��9�I�s\��J�u{1-E�*R.�xwt��b	��D���;Z1W����jvą�Y��J+v;���\�+��N^�ǰc�70A�^8�l�HV�F ��USZ�۵ou�TB_F^��*��r!Vݒ��b�h�hgŠ]k1�����hOB�j�&�H�Ǥ�r0;~q
���ܖ�9�DeQS��tJ�4U�!�ɣ`��H��E������ܔ�b9�� ަT�M��T� v�T(�V܊HP��5!���u09.U�e���R`LHo���rl�C #`$r�r�l�k΋�-Y}W�f�p��Ң]���p��츻P,i�H�'�7,����0*��"-��g�-\���q���$�ݱ�n��2X	�����/�Y�	����yP�s�`%�H�kIH��u3;ॼx�\}��I��@jMyQD9Y0v���-z;4�x`����q�VZAhq�t���	*���ɭ	�n�i%�o!�|�����x.�,�Z$������t�	�[| 紖Nks���HC���U/�&3U��I��[����2�^�vh�Q��j���h	-��g)̊�B�b�i%f-���YP"ݞQ�� %����e5 5!E�=c(���R
(��F
�bR�Q������ʉS�a�k`�
m�^o����=��<���)A�;�C-�:ht�7V�q<��<p��-�=(=ޝ������;���ҕy      Z   �   x���˪�0�}�b�F�r�\���hsd�&2g�6
Ŗƾ������0���Vj6 ���6W�!П���s;:S���A�_���7�-�m7��ݭ���u1S)",P���YV%T�pR�ia���D�I��ֻ�:÷��z�p$�#Y�2�'@,�Q2�v��1�g�Q��,'s,�YH�����B>#KJ�X=?D�������C�}c{�= 3=H      h   �   x���v
Q���WP*(M��LV�S�/JI-rN,IM�/�TR�P�LQ�QP�K�M�.���E�%��yJ�
�a�AA�.�~�
���!��
a�>��2XA�PGA=$��DHC��LT Z����P�Z�P�Դ��[��nQ0R���D	�:y��|c�`5F	Q.. Y��      e   �   x��н
�0�ݧ�dт��5v*J�U0�Щ�5�`Al}���,.��qtiTmA��4L��{"���u�A�t�B@f��s�m����^��jT]�B�G0Wc���颖k �!��3�xD���@i�hK,�Hh�����B�BH�$�`N�,�I��&B�b�1ɷ��k��%2�l�x�.���      g   �   x���v
Q���WP*(M��LV�S�/JI-*��,.QR�Pr�+)���LQ�Q��@ٞy�E�)��y%P���ļ�̒J%M�0נ OO?w����W_�0G�PW��a�� D@RӚ�s�av������XG��L�&:
��`Gt|�� �.. �j�6      o   �   x����
�@�O1�Ia75�NaK,����)J%jWt=���X�#8�a>.?�,�iV K�P;��B6:t�*u�峕=W\
&b5��{N���y#���p���8ǲWȂ��Y�6,�B~���r�;ѩ�`��a�bX`p������>,F<;��8ڣY����#d��#l��\'��PK��d��      m   �   x���v
Q���WP*(M��LV�S
(�O)M.)VR�P�LQ�Q����%�"��2�Q��KP��%���E� 1����b%M�0נ OO?w����W_�0G�PW��ah�����������`h �z&��H���93	$���i��9(}b
�IQf^1�# ���#��z.. �r�      a   �  x���Mk�@�Š�HM�$NBO+i��fw%�/!m1ul�4��wF-=��\�"V����K!c*@�D�N���Oռʯ��~���R�YE���]z��[)���fZ=S	�w{Y���KuqĔ��`!��F��y�pvy��C�[O+���p�r8~?�{����w3���+�x,)�!_9~0��߼dH�{�n=�qɢ�c�eH!�b�c�c�)���-K��Z!�:2B}���ZTDnX�tJ2�Gv�E��^HJ:�"���ڤ�Y�Z��_&����I�~9�o�Ƹ��\��x��AZҲ�7�Wf
����?:���,n6�NX�)?��L=oߓ��H���=��I^�Q�H��������`I҄���-A�y�k2K|z3o�lC�����(c���7�W�˛ۿ�g��
W�      ]      x���v
Q���WP*(M��LV�S
��,)�O�MM,.-JUR�P�LQ�QPrI-N.�,(���S�T�s
�t��sW�q�Us�	u���
�:
�>!��\�4��h��;�m1���	� I�X�      q   p   x���v
Q���WP*(M��LV�S
-N-*VR�P�LQ�Q s�sSA�������%M�0נ OO?w����W_�0G�PW��a���P��W�c+�kZsqq @�!U     