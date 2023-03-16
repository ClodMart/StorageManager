PGDMP         ;                {            GestioneMagazzino    15.2    15.2 =    J           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            K           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            L           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            M           1262    16566    GestioneMagazzino    DATABASE     �   CREATE DATABASE "GestioneMagazzino" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Italian_Italy.1252';
 #   DROP DATABASE "GestioneMagazzino";
                postgres    false                        2615    16567    dbo    SCHEMA        CREATE SCHEMA dbo;
    DROP SCHEMA dbo;
                postgres    false            �            1259    16569    Drink_Ingredients    TABLE     �  CREATE TABLE dbo."Drink_Ingredients" (
    "Id" integer NOT NULL,
    "Drink_Name" character varying(255) NOT NULL,
    "Category" character varying(50) NOT NULL,
    "IsUsed" integer NOT NULL,
    "Supplier_Id" integer,
    "Cost__" numeric(20,2),
    "OldCost__" numeric(20,2) DEFAULT 0 NOT NULL,
    "Size_Liters" numeric(20,2) DEFAULT 1,
    "Size_Units" numeric(20,2) DEFAULT 1,
    "Cost___Liter" numeric(40,20),
    "Cost___Unit" numeric(40,20),
    "Quantity_Needed" integer NOT NULL,
    "Actual_Quantity" integer DEFAULT '-1'::integer NOT NULL,
    "Notes" character varying(255),
    "IsEnough" integer,
    "CostDifference" numeric(21,2)
);
 $   DROP TABLE dbo."Drink_Ingredients";
       dbo         heap    postgres    false    5            �            1259    16568    Drink_Ingredients_Id_seq    SEQUENCE     �   CREATE SEQUENCE dbo."Drink_Ingredients_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE dbo."Drink_Ingredients_Id_seq";
       dbo          postgres    false    215    5            N           0    0    Drink_Ingredients_Id_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE dbo."Drink_Ingredients_Id_seq" OWNED BY dbo."Drink_Ingredients"."Id";
          dbo          postgres    false    214            �            1259    16583    Ingredients    TABLE     �  CREATE TABLE dbo."Ingredients" (
    "Id" integer NOT NULL,
    "Ingredient" character varying(150) NOT NULL,
    "Category" character varying(50) NOT NULL,
    "Is_Used" integer,
    "Supplier_Id" integer NOT NULL,
    "Size_Kg" numeric(20,3) DEFAULT 1 NOT NULL,
    "Size_Units" integer NOT NULL,
    "Cost__" numeric(20,2) NOT NULL,
    "OldCost__" numeric(20,2) DEFAULT 0 NOT NULL,
    "Cost___Kg" numeric(40,19),
    "Cost___Unit" numeric(31,13),
    "Quantity_Needed" integer NOT NULL,
    "Actual_Quantity" integer DEFAULT '-1'::integer NOT NULL,
    "Notes" character varying(255),
    "IsEnough" integer,
    "CostDifference" numeric(21,2),
    "LastOrderDateTime" date
);
    DROP TABLE dbo."Ingredients";
       dbo         heap    postgres    false    5            �            1259    16582    Ingredients_Id_seq    SEQUENCE     �   CREATE SEQUENCE dbo."Ingredients_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE dbo."Ingredients_Id_seq";
       dbo          postgres    false    217    5            O           0    0    Ingredients_Id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE dbo."Ingredients_Id_seq" OWNED BY dbo."Ingredients"."Id";
          dbo          postgres    false    216            �            1259    16596    IsUsed_Values    TABLE     r   CREATE TABLE dbo."IsUsed_Values" (
    "Id" integer NOT NULL,
    "Description" character varying(50) NOT NULL
);
     DROP TABLE dbo."IsUsed_Values";
       dbo         heap    postgres    false    5            �            1259    16595    IsUsed_Values_Id_seq    SEQUENCE     �   CREATE SEQUENCE dbo."IsUsed_Values_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE dbo."IsUsed_Values_Id_seq";
       dbo          postgres    false    5    219            P           0    0    IsUsed_Values_Id_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE dbo."IsUsed_Values_Id_seq" OWNED BY dbo."IsUsed_Values"."Id";
          dbo          postgres    false    218            �            1259    16604    Menu    TABLE     �   CREATE TABLE dbo."Menu" (
    "Id" integer NOT NULL,
    "Menu_Entry" character varying(255) NOT NULL,
    "Category" character varying(255) NOT NULL,
    "SellingPrice" numeric(20,2)
);
    DROP TABLE dbo."Menu";
       dbo         heap    postgres    false    5            �            1259    16614    MenuPreparations    TABLE     �   CREATE TABLE dbo."MenuPreparations" (
    "Entry_Id" integer NOT NULL,
    "Menu_Product_Id" integer NOT NULL,
    "Ingedient_Id" integer NOT NULL,
    "IngredientQuantity" numeric(20,2),
    "UnitOfMesure" integer NOT NULL
);
 #   DROP TABLE dbo."MenuPreparations";
       dbo         heap    postgres    false    5            �            1259    16613    MenuPreparations_Entry_Id_seq    SEQUENCE     �   CREATE SEQUENCE dbo."MenuPreparations_Entry_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 3   DROP SEQUENCE dbo."MenuPreparations_Entry_Id_seq";
       dbo          postgres    false    5    223            Q           0    0    MenuPreparations_Entry_Id_seq    SEQUENCE OWNED BY     _   ALTER SEQUENCE dbo."MenuPreparations_Entry_Id_seq" OWNED BY dbo."MenuPreparations"."Entry_Id";
          dbo          postgres    false    222            �            1259    16603    Menu_Id_seq    SEQUENCE     �   CREATE SEQUENCE dbo."Menu_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 !   DROP SEQUENCE dbo."Menu_Id_seq";
       dbo          postgres    false    221    5            R           0    0    Menu_Id_seq    SEQUENCE OWNED BY     ;   ALTER SEQUENCE dbo."Menu_Id_seq" OWNED BY dbo."Menu"."Id";
          dbo          postgres    false    220            �            1259    16622 	   Suppliers    TABLE       CREATE TABLE dbo."Suppliers" (
    "Id" integer NOT NULL,
    "Supplier_Name" character varying(150) NOT NULL,
    "PT_IVA" character varying(50),
    "Telefono" character varying(50),
    "Email" character varying(50),
    "Note" character varying(255)
);
    DROP TABLE dbo."Suppliers";
       dbo         heap    postgres    false    5            �            1259    16621    Suppliers_Id_seq    SEQUENCE     �   CREATE SEQUENCE dbo."Suppliers_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE dbo."Suppliers_Id_seq";
       dbo          postgres    false    225    5            S           0    0    Suppliers_Id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE dbo."Suppliers_Id_seq" OWNED BY dbo."Suppliers"."Id";
          dbo          postgres    false    224            �            1259    16632    UnitsOfMesure    TABLE     r   CREATE TABLE dbo."UnitsOfMesure" (
    "Id" integer NOT NULL,
    "Description" character varying(50) NOT NULL
);
     DROP TABLE dbo."UnitsOfMesure";
       dbo         heap    postgres    false    5            �            1259    16631    UnitsOfMesure_Id_seq    SEQUENCE     �   CREATE SEQUENCE dbo."UnitsOfMesure_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE dbo."UnitsOfMesure_Id_seq";
       dbo          postgres    false    5    227            T           0    0    UnitsOfMesure_Id_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE dbo."UnitsOfMesure_Id_seq" OWNED BY dbo."UnitsOfMesure"."Id";
          dbo          postgres    false    226            �            1259    16640    Use_Materials    TABLE     Q  CREATE TABLE dbo."Use_Materials" (
    "Id" integer NOT NULL,
    "Material_Name" character varying(255) NOT NULL,
    "Category" character varying(255) NOT NULL,
    "Supplier_Id" integer NOT NULL,
    "IsUsed" integer NOT NULL,
    "Size_Units" numeric(20,2) DEFAULT 1,
    "Cost__" numeric(20,2) NOT NULL,
    "OldCost__" numeric(20,2) DEFAULT 0 NOT NULL,
    "Cost___Unit" numeric(40,20),
    "Quantity_Needed" integer NOT NULL,
    "Actual_Quantity" integer DEFAULT '-1'::integer NOT NULL,
    "Notes" character varying(255),
    "IsEnough" integer,
    "CostDifference" numeric(21,2)
);
     DROP TABLE dbo."Use_Materials";
       dbo         heap    postgres    false    5            �            1259    16639    Use_Materials_Id_seq    SEQUENCE     �   CREATE SEQUENCE dbo."Use_Materials_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE dbo."Use_Materials_Id_seq";
       dbo          postgres    false    229    5            U           0    0    Use_Materials_Id_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE dbo."Use_Materials_Id_seq" OWNED BY dbo."Use_Materials"."Id";
          dbo          postgres    false    228            �           2604    16572    Drink_Ingredients Id    DEFAULT     |   ALTER TABLE ONLY dbo."Drink_Ingredients" ALTER COLUMN "Id" SET DEFAULT nextval('dbo."Drink_Ingredients_Id_seq"'::regclass);
 D   ALTER TABLE dbo."Drink_Ingredients" ALTER COLUMN "Id" DROP DEFAULT;
       dbo          postgres    false    215    214    215            �           2604    16586    Ingredients Id    DEFAULT     p   ALTER TABLE ONLY dbo."Ingredients" ALTER COLUMN "Id" SET DEFAULT nextval('dbo."Ingredients_Id_seq"'::regclass);
 >   ALTER TABLE dbo."Ingredients" ALTER COLUMN "Id" DROP DEFAULT;
       dbo          postgres    false    216    217    217            �           2604    16599    IsUsed_Values Id    DEFAULT     t   ALTER TABLE ONLY dbo."IsUsed_Values" ALTER COLUMN "Id" SET DEFAULT nextval('dbo."IsUsed_Values_Id_seq"'::regclass);
 @   ALTER TABLE dbo."IsUsed_Values" ALTER COLUMN "Id" DROP DEFAULT;
       dbo          postgres    false    219    218    219            �           2604    16607    Menu Id    DEFAULT     b   ALTER TABLE ONLY dbo."Menu" ALTER COLUMN "Id" SET DEFAULT nextval('dbo."Menu_Id_seq"'::regclass);
 7   ALTER TABLE dbo."Menu" ALTER COLUMN "Id" DROP DEFAULT;
       dbo          postgres    false    220    221    221            �           2604    16617    MenuPreparations Entry_Id    DEFAULT     �   ALTER TABLE ONLY dbo."MenuPreparations" ALTER COLUMN "Entry_Id" SET DEFAULT nextval('dbo."MenuPreparations_Entry_Id_seq"'::regclass);
 I   ALTER TABLE dbo."MenuPreparations" ALTER COLUMN "Entry_Id" DROP DEFAULT;
       dbo          postgres    false    223    222    223            �           2604    16625    Suppliers Id    DEFAULT     l   ALTER TABLE ONLY dbo."Suppliers" ALTER COLUMN "Id" SET DEFAULT nextval('dbo."Suppliers_Id_seq"'::regclass);
 <   ALTER TABLE dbo."Suppliers" ALTER COLUMN "Id" DROP DEFAULT;
       dbo          postgres    false    224    225    225            �           2604    16635    UnitsOfMesure Id    DEFAULT     t   ALTER TABLE ONLY dbo."UnitsOfMesure" ALTER COLUMN "Id" SET DEFAULT nextval('dbo."UnitsOfMesure_Id_seq"'::regclass);
 @   ALTER TABLE dbo."UnitsOfMesure" ALTER COLUMN "Id" DROP DEFAULT;
       dbo          postgres    false    226    227    227            �           2604    16643    Use_Materials Id    DEFAULT     t   ALTER TABLE ONLY dbo."Use_Materials" ALTER COLUMN "Id" SET DEFAULT nextval('dbo."Use_Materials_Id_seq"'::regclass);
 @   ALTER TABLE dbo."Use_Materials" ALTER COLUMN "Id" DROP DEFAULT;
       dbo          postgres    false    229    228    229            9          0    16569    Drink_Ingredients 
   TABLE DATA             COPY dbo."Drink_Ingredients" ("Id", "Drink_Name", "Category", "IsUsed", "Supplier_Id", "Cost__", "OldCost__", "Size_Liters", "Size_Units", "Cost___Liter", "Cost___Unit", "Quantity_Needed", "Actual_Quantity", "Notes", "IsEnough", "CostDifference") FROM stdin;
    dbo          postgres    false    215   wJ       ;          0    16583    Ingredients 
   TABLE DATA             COPY dbo."Ingredients" ("Id", "Ingredient", "Category", "Is_Used", "Supplier_Id", "Size_Kg", "Size_Units", "Cost__", "OldCost__", "Cost___Kg", "Cost___Unit", "Quantity_Needed", "Actual_Quantity", "Notes", "IsEnough", "CostDifference", "LastOrderDateTime") FROM stdin;
    dbo          postgres    false    217   �J       =          0    16596    IsUsed_Values 
   TABLE DATA           ;   COPY dbo."IsUsed_Values" ("Id", "Description") FROM stdin;
    dbo          postgres    false    219   qP       ?          0    16604    Menu 
   TABLE DATA           M   COPY dbo."Menu" ("Id", "Menu_Entry", "Category", "SellingPrice") FROM stdin;
    dbo          postgres    false    221   �P       A          0    16614    MenuPreparations 
   TABLE DATA           ~   COPY dbo."MenuPreparations" ("Entry_Id", "Menu_Product_Id", "Ingedient_Id", "IngredientQuantity", "UnitOfMesure") FROM stdin;
    dbo          postgres    false    223   �P       C          0    16622 	   Suppliers 
   TABLE DATA           `   COPY dbo."Suppliers" ("Id", "Supplier_Name", "PT_IVA", "Telefono", "Email", "Note") FROM stdin;
    dbo          postgres    false    225   Q       E          0    16632    UnitsOfMesure 
   TABLE DATA           ;   COPY dbo."UnitsOfMesure" ("Id", "Description") FROM stdin;
    dbo          postgres    false    227   GR       G          0    16640    Use_Materials 
   TABLE DATA           �   COPY dbo."Use_Materials" ("Id", "Material_Name", "Category", "Supplier_Id", "IsUsed", "Size_Units", "Cost__", "OldCost__", "Cost___Unit", "Quantity_Needed", "Actual_Quantity", "Notes", "IsEnough", "CostDifference") FROM stdin;
    dbo          postgres    false    229   xR       V           0    0    Drink_Ingredients_Id_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('dbo."Drink_Ingredients_Id_seq"', 1, false);
          dbo          postgres    false    214            W           0    0    Ingredients_Id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('dbo."Ingredients_Id_seq"', 51, true);
          dbo          postgres    false    216            X           0    0    IsUsed_Values_Id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('dbo."IsUsed_Values_Id_seq"', 12, true);
          dbo          postgres    false    218            Y           0    0    MenuPreparations_Entry_Id_seq    SEQUENCE SET     K   SELECT pg_catalog.setval('dbo."MenuPreparations_Entry_Id_seq"', 1, false);
          dbo          postgres    false    222            Z           0    0    Menu_Id_seq    SEQUENCE SET     9   SELECT pg_catalog.setval('dbo."Menu_Id_seq"', 1, false);
          dbo          postgres    false    220            [           0    0    Suppliers_Id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('dbo."Suppliers_Id_seq"', 52, true);
          dbo          postgres    false    224            \           0    0    UnitsOfMesure_Id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('dbo."UnitsOfMesure_Id_seq"', 17, true);
          dbo          postgres    false    226            ]           0    0    Use_Materials_Id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('dbo."Use_Materials_Id_seq"', 1, false);
          dbo          postgres    false    228            �           2606    16581 &   Drink_Ingredients PK_Drink_Ingredients 
   CONSTRAINT     g   ALTER TABLE ONLY dbo."Drink_Ingredients"
    ADD CONSTRAINT "PK_Drink_Ingredients" PRIMARY KEY ("Id");
 Q   ALTER TABLE ONLY dbo."Drink_Ingredients" DROP CONSTRAINT "PK_Drink_Ingredients";
       dbo            postgres    false    215            �           2606    16594    Ingredients PK_Ingredients 
   CONSTRAINT     [   ALTER TABLE ONLY dbo."Ingredients"
    ADD CONSTRAINT "PK_Ingredients" PRIMARY KEY ("Id");
 E   ALTER TABLE ONLY dbo."Ingredients" DROP CONSTRAINT "PK_Ingredients";
       dbo            postgres    false    217            �           2606    16602    IsUsed_Values PK_IsUsed_Values 
   CONSTRAINT     _   ALTER TABLE ONLY dbo."IsUsed_Values"
    ADD CONSTRAINT "PK_IsUsed_Values" PRIMARY KEY ("Id");
 I   ALTER TABLE ONLY dbo."IsUsed_Values" DROP CONSTRAINT "PK_IsUsed_Values";
       dbo            postgres    false    219            �           2606    16612    Menu PK_Menu 
   CONSTRAINT     M   ALTER TABLE ONLY dbo."Menu"
    ADD CONSTRAINT "PK_Menu" PRIMARY KEY ("Id");
 7   ALTER TABLE ONLY dbo."Menu" DROP CONSTRAINT "PK_Menu";
       dbo            postgres    false    221            �           2606    16620 $   MenuPreparations PK_MenuPreparations 
   CONSTRAINT     k   ALTER TABLE ONLY dbo."MenuPreparations"
    ADD CONSTRAINT "PK_MenuPreparations" PRIMARY KEY ("Entry_Id");
 O   ALTER TABLE ONLY dbo."MenuPreparations" DROP CONSTRAINT "PK_MenuPreparations";
       dbo            postgres    false    223            �           2606    16630    Suppliers PK_Suppliers 
   CONSTRAINT     W   ALTER TABLE ONLY dbo."Suppliers"
    ADD CONSTRAINT "PK_Suppliers" PRIMARY KEY ("Id");
 A   ALTER TABLE ONLY dbo."Suppliers" DROP CONSTRAINT "PK_Suppliers";
       dbo            postgres    false    225            �           2606    16638    UnitsOfMesure PK_UnitsOfMesure 
   CONSTRAINT     _   ALTER TABLE ONLY dbo."UnitsOfMesure"
    ADD CONSTRAINT "PK_UnitsOfMesure" PRIMARY KEY ("Id");
 I   ALTER TABLE ONLY dbo."UnitsOfMesure" DROP CONSTRAINT "PK_UnitsOfMesure";
       dbo            postgres    false    227            �           2606    16651    Use_Materials PK_Use_Materials 
   CONSTRAINT     _   ALTER TABLE ONLY dbo."Use_Materials"
    ADD CONSTRAINT "PK_Use_Materials" PRIMARY KEY ("Id");
 I   ALTER TABLE ONLY dbo."Use_Materials" DROP CONSTRAINT "PK_Use_Materials";
       dbo            postgres    false    229            9      x������ � �      ;   �  x����n�8���S𲷆�H^
���
�%C�{z	
w��m���>�>�>�IɖE��Y��H9�_��v]��[��Po��}մ���)���UF3�g3B����Aj�K2�����)�U�[$C۾Z����=#�]
�+���E�����d+~1zvw '8"{ו[��v�"�(M������^Ē��q[�����5����3[�t�i��n���8��~1nvw������$G�fw�q:�MY�**G�#�Mir1mvw�fF�*�ZS��HY��� �m�'�Cmʻ�6�J�qr�
3s�"fwg���𩹢4+��f1@V**,�Բ��]L<�E6l�z
�`�H<4�Iˉǭ�g�������d.��c�	���RQq�A��c��a��s�>n��:������,��%D����L�p��V���mP`�	U�	��ش���US"��m߷���{ՙs]��2��k}���vu�`���n�9���:�X��2�h��쟟�S�I�� 1A�P�b�S��#i�|�:� 4�ئp�~�Z8��ne�]_�EE���e���tx�"��r��)��L�0�S�k�	2����O����}����"�AsSv=�EQ�T60��Z(�ʉ�Bi���e�+	������w����������.<?�?��?<!،D��������i�>h�`M� �Mv�vk{{;,��5�r��a�H�^y�94^����7(k[A����uUX8��v�!^!`BFȃ�3�џW��m��:�g�s�Sn9}٠��,>�1Ƅ�:�Άh���\h-�ɩ�Ay~�:�@.��������?�5A@̓9�^��"�u]`q�n�'Y<<,H�Ri�+�s���we�Y����8��c�*�-�5ЎtU� �)�����;3/\�H����%�0Ȝ�0�B�� _�&�������U�����T�0�p�0ދS%⮭[�Bw��f�B/�O�2:�S�B&�q�t��_`<9@B�>U?��<L]�Q,�^U�A=����q:��ڮ�3E�)J��T��_#��=uDH�M�*;r���X�
8�W�7�(9aB�=e*��M�y�n6;(�в�C��ں8�
�'1�$A�t��t�M��U.�J�� ��egEw�6q]G�PD��g]�.�:�fQ�9`P2%	��G�F��bd���������!zb��<��G�:�5��je����$�mM3��`Pa�ɹ��Hj�6�)�r��	�{�R�uhB]� [�l&"����h.�pg�!SZ:��s��e:U���@ݳ�ue��џ�R���IϘQphm��׫��|ό�}�?������c�^��N�!LD8������RAB��
V�H�����`�(��$��-���iZ�2s� ������{e��l�m&97�0�����!`�e(�z�����$㕉�xI�W�O����_� X3      =   _   x��AD@F�u�S�	���AS)$*V�f���ˤ�/UAK��p[������pWtdk�P|.5Гr�V��8]����f~g�\�'ѿ� g��      ?      x������ � �      A      x������ � �      C     x���;n�0��>��N5[y��M�D%+�� z���ׯ���I�G���n㄰޾n��ۿs�4�(%���*��5	�x�u|�@Y��������~���Kc]v�Vq�u�Cf'1���A';�<�s�%�v��x4#-YГ����\���.����Q2%���T��	GR��c0��@R��v��q�Uc���\P(�R��>
��G	;�^�/bw@"d��۽p��{��[Q%��P߅=B�4gF��`O �%U�6�J��������m���T#      E   !   x�34��	�24��v�24�������� =(�      G      x������ � �     