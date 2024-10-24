PGDMP         8                {            GestioneMagazzino    15.2    15.2 +    >           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            ?           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            @           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            A           1262    16566    GestioneMagazzino    DATABASE     �   CREATE DATABASE "GestioneMagazzino" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Italian_Italy.1252';
 #   DROP DATABASE "GestioneMagazzino";
                postgres    false                        2615    16567    dbo    SCHEMA        CREATE SCHEMA "dbo";
    DROP SCHEMA "dbo";
                postgres    false            �            1259    16569    Drink_Ingredients    TABLE     �  CREATE TABLE "dbo"."Drink_Ingredients" (
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
 &   DROP TABLE "dbo"."Drink_Ingredients";
       dbo         heap    postgres    false    5            �            1259    16568    Drink_Ingredients_Id_seq    SEQUENCE     �   CREATE SEQUENCE "dbo"."Drink_Ingredients_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE "dbo"."Drink_Ingredients_Id_seq";
       dbo          postgres    false    5    215            B           0    0    Drink_Ingredients_Id_seq    SEQUENCE OWNED BY     Y   ALTER SEQUENCE "dbo"."Drink_Ingredients_Id_seq" OWNED BY "dbo"."Drink_Ingredients"."Id";
          dbo          postgres    false    214            �            1259    16708    Ingredients_Id_seq    SEQUENCE     �   CREATE SEQUENCE "dbo"."Ingredients_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE "dbo"."Ingredients_Id_seq";
       dbo          postgres    false    5            �            1259    16709    Ingredients    TABLE       CREATE TABLE "dbo"."Ingredients" (
    "Id" integer DEFAULT "nextval"('"dbo"."Ingredients_Id_seq"'::"regclass") NOT NULL,
    "Ingredient" character varying(150) NOT NULL,
    "Category" character varying(50) NOT NULL,
    "Is_Used" integer,
    "Supplier_Id" integer NOT NULL,
    "Size_Kg" numeric(20,3) DEFAULT 1 NOT NULL,
    "Size_Units" integer NOT NULL,
    "Cost_€" numeric(20,3) NOT NULL,
    "OldCost_€" numeric(20,3) DEFAULT 0 NOT NULL,
    "Cost_€/Kg" numeric(10,3) GENERATED ALWAYS AS (("Cost_€" / "Size_Kg")) STORED,
    "Cost_€/Unit" numeric(10,3) GENERATED ALWAYS AS (("Cost_€" / ("Size_Units")::numeric)) STORED,
    "Quantity_Needed" integer NOT NULL,
    "Actual_Quantity" integer DEFAULT '-1'::integer NOT NULL,
    "Notes" character varying(255),
    "IsEnough" boolean GENERATED ALWAYS AS (
CASE
    WHEN ("Quantity_Needed" > "Actual_Quantity") THEN false
    ELSE true
END) STORED,
    "CostDifference" numeric(10,3) GENERATED ALWAYS AS (("OldCost_€" - "Cost_€")) STORED,
    "LastOrderDateTime" "date"
);
     DROP TABLE "dbo"."Ingredients";
       dbo         heap    postgres    false    228    5            �            1259    16596    IsUsed_Values    TABLE     t   CREATE TABLE "dbo"."IsUsed_Values" (
    "Id" integer NOT NULL,
    "Description" character varying(50) NOT NULL
);
 "   DROP TABLE "dbo"."IsUsed_Values";
       dbo         heap    postgres    false    5            �            1259    16595    IsUsed_Values_Id_seq    SEQUENCE     �   CREATE SEQUENCE "dbo"."IsUsed_Values_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 ,   DROP SEQUENCE "dbo"."IsUsed_Values_Id_seq";
       dbo          postgres    false    5    217            C           0    0    IsUsed_Values_Id_seq    SEQUENCE OWNED BY     Q   ALTER SEQUENCE "dbo"."IsUsed_Values_Id_seq" OWNED BY "dbo"."IsUsed_Values"."Id";
          dbo          postgres    false    216            �            1259    16604    Menu    TABLE     �   CREATE TABLE "dbo"."Menu" (
    "Id" integer NOT NULL,
    "Menu_Entry" character varying(255) NOT NULL,
    "Category" character varying(255) NOT NULL,
    "SellingPrice" numeric(20,2)
);
    DROP TABLE "dbo"."Menu";
       dbo         heap    postgres    false    5            �            1259    16614    MenuPreparations    TABLE     �   CREATE TABLE "dbo"."MenuPreparations" (
    "Entry_Id" integer NOT NULL,
    "Menu_Product_Id" integer NOT NULL,
    "Ingedient_Id" integer NOT NULL,
    "IngredientQuantity" numeric(20,2),
    "UnitOfMesure" integer NOT NULL
);
 %   DROP TABLE "dbo"."MenuPreparations";
       dbo         heap    postgres    false    5            �            1259    16613    MenuPreparations_Entry_Id_seq    SEQUENCE     �   CREATE SEQUENCE "dbo"."MenuPreparations_Entry_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 5   DROP SEQUENCE "dbo"."MenuPreparations_Entry_Id_seq";
       dbo          postgres    false    221    5            D           0    0    MenuPreparations_Entry_Id_seq    SEQUENCE OWNED BY     c   ALTER SEQUENCE "dbo"."MenuPreparations_Entry_Id_seq" OWNED BY "dbo"."MenuPreparations"."Entry_Id";
          dbo          postgres    false    220            �            1259    16603    Menu_Id_seq    SEQUENCE     �   CREATE SEQUENCE "dbo"."Menu_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE "dbo"."Menu_Id_seq";
       dbo          postgres    false    219    5            E           0    0    Menu_Id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE "dbo"."Menu_Id_seq" OWNED BY "dbo"."Menu"."Id";
          dbo          postgres    false    218            �            1259    16622 	   Suppliers    TABLE       CREATE TABLE "dbo"."Suppliers" (
    "Id" integer NOT NULL,
    "Supplier_Name" character varying(150) NOT NULL,
    "PT_IVA" character varying(50),
    "Telefono" character varying(50),
    "Email" character varying(50),
    "Note" character varying(255)
);
    DROP TABLE "dbo"."Suppliers";
       dbo         heap    postgres    false    5            �            1259    16621    Suppliers_Id_seq    SEQUENCE     �   CREATE SEQUENCE "dbo"."Suppliers_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE "dbo"."Suppliers_Id_seq";
       dbo          postgres    false    5    223            F           0    0    Suppliers_Id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE "dbo"."Suppliers_Id_seq" OWNED BY "dbo"."Suppliers"."Id";
          dbo          postgres    false    222            �            1259    16632    UnitsOfMesure    TABLE     t   CREATE TABLE "dbo"."UnitsOfMesure" (
    "Id" integer NOT NULL,
    "Description" character varying(50) NOT NULL
);
 "   DROP TABLE "dbo"."UnitsOfMesure";
       dbo         heap    postgres    false    5            �            1259    16631    UnitsOfMesure_Id_seq    SEQUENCE     �   CREATE SEQUENCE "dbo"."UnitsOfMesure_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 ,   DROP SEQUENCE "dbo"."UnitsOfMesure_Id_seq";
       dbo          postgres    false    225    5            G           0    0    UnitsOfMesure_Id_seq    SEQUENCE OWNED BY     Q   ALTER SEQUENCE "dbo"."UnitsOfMesure_Id_seq" OWNED BY "dbo"."UnitsOfMesure"."Id";
          dbo          postgres    false    224            �            1259    16640    Use_Materials    TABLE     S  CREATE TABLE "dbo"."Use_Materials" (
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
 "   DROP TABLE "dbo"."Use_Materials";
       dbo         heap    postgres    false    5            �            1259    16639    Use_Materials_Id_seq    SEQUENCE     �   CREATE SEQUENCE "dbo"."Use_Materials_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 ,   DROP SEQUENCE "dbo"."Use_Materials_Id_seq";
       dbo          postgres    false    227    5            H           0    0    Use_Materials_Id_seq    SEQUENCE OWNED BY     Q   ALTER SEQUENCE "dbo"."Use_Materials_Id_seq" OWNED BY "dbo"."Use_Materials"."Id";
          dbo          postgres    false    226            �           2604    16572    Drink_Ingredients Id    DEFAULT     �   ALTER TABLE ONLY "dbo"."Drink_Ingredients" ALTER COLUMN "Id" SET DEFAULT "nextval"('"dbo"."Drink_Ingredients_Id_seq"'::"regclass");
 F   ALTER TABLE "dbo"."Drink_Ingredients" ALTER COLUMN "Id" DROP DEFAULT;
       dbo          postgres    false    215    214    215            �           2604    16599    IsUsed_Values Id    DEFAULT     |   ALTER TABLE ONLY "dbo"."IsUsed_Values" ALTER COLUMN "Id" SET DEFAULT "nextval"('"dbo"."IsUsed_Values_Id_seq"'::"regclass");
 B   ALTER TABLE "dbo"."IsUsed_Values" ALTER COLUMN "Id" DROP DEFAULT;
       dbo          postgres    false    216    217    217            �           2604    16607    Menu Id    DEFAULT     j   ALTER TABLE ONLY "dbo"."Menu" ALTER COLUMN "Id" SET DEFAULT "nextval"('"dbo"."Menu_Id_seq"'::"regclass");
 9   ALTER TABLE "dbo"."Menu" ALTER COLUMN "Id" DROP DEFAULT;
       dbo          postgres    false    219    218    219            �           2604    16617    MenuPreparations Entry_Id    DEFAULT     �   ALTER TABLE ONLY "dbo"."MenuPreparations" ALTER COLUMN "Entry_Id" SET DEFAULT "nextval"('"dbo"."MenuPreparations_Entry_Id_seq"'::"regclass");
 K   ALTER TABLE "dbo"."MenuPreparations" ALTER COLUMN "Entry_Id" DROP DEFAULT;
       dbo          postgres    false    221    220    221            �           2604    16625    Suppliers Id    DEFAULT     t   ALTER TABLE ONLY "dbo"."Suppliers" ALTER COLUMN "Id" SET DEFAULT "nextval"('"dbo"."Suppliers_Id_seq"'::"regclass");
 >   ALTER TABLE "dbo"."Suppliers" ALTER COLUMN "Id" DROP DEFAULT;
       dbo          postgres    false    222    223    223            �           2604    16635    UnitsOfMesure Id    DEFAULT     |   ALTER TABLE ONLY "dbo"."UnitsOfMesure" ALTER COLUMN "Id" SET DEFAULT "nextval"('"dbo"."UnitsOfMesure_Id_seq"'::"regclass");
 B   ALTER TABLE "dbo"."UnitsOfMesure" ALTER COLUMN "Id" DROP DEFAULT;
       dbo          postgres    false    224    225    225            �           2604    16643    Use_Materials Id    DEFAULT     |   ALTER TABLE ONLY "dbo"."Use_Materials" ALTER COLUMN "Id" SET DEFAULT "nextval"('"dbo"."Use_Materials_Id_seq"'::"regclass");
 B   ALTER TABLE "dbo"."Use_Materials" ALTER COLUMN "Id" DROP DEFAULT;
       dbo          postgres    false    227    226    227            �           2606    16581 &   Drink_Ingredients PK_Drink_Ingredients 
   CONSTRAINT     i   ALTER TABLE ONLY "dbo"."Drink_Ingredients"
    ADD CONSTRAINT "PK_Drink_Ingredients" PRIMARY KEY ("Id");
 S   ALTER TABLE ONLY "dbo"."Drink_Ingredients" DROP CONSTRAINT "PK_Drink_Ingredients";
       dbo            postgres    false    215            �           2606    16721    Ingredients PK_Ingredients 
   CONSTRAINT     ]   ALTER TABLE ONLY "dbo"."Ingredients"
    ADD CONSTRAINT "PK_Ingredients" PRIMARY KEY ("Id");
 G   ALTER TABLE ONLY "dbo"."Ingredients" DROP CONSTRAINT "PK_Ingredients";
       dbo            postgres    false    229            �           2606    16602    IsUsed_Values PK_IsUsed_Values 
   CONSTRAINT     a   ALTER TABLE ONLY "dbo"."IsUsed_Values"
    ADD CONSTRAINT "PK_IsUsed_Values" PRIMARY KEY ("Id");
 K   ALTER TABLE ONLY "dbo"."IsUsed_Values" DROP CONSTRAINT "PK_IsUsed_Values";
       dbo            postgres    false    217            �           2606    16612    Menu PK_Menu 
   CONSTRAINT     O   ALTER TABLE ONLY "dbo"."Menu"
    ADD CONSTRAINT "PK_Menu" PRIMARY KEY ("Id");
 9   ALTER TABLE ONLY "dbo"."Menu" DROP CONSTRAINT "PK_Menu";
       dbo            postgres    false    219            �           2606    16620 $   MenuPreparations PK_MenuPreparations 
   CONSTRAINT     m   ALTER TABLE ONLY "dbo"."MenuPreparations"
    ADD CONSTRAINT "PK_MenuPreparations" PRIMARY KEY ("Entry_Id");
 Q   ALTER TABLE ONLY "dbo"."MenuPreparations" DROP CONSTRAINT "PK_MenuPreparations";
       dbo            postgres    false    221            �           2606    16630    Suppliers PK_Suppliers 
   CONSTRAINT     Y   ALTER TABLE ONLY "dbo"."Suppliers"
    ADD CONSTRAINT "PK_Suppliers" PRIMARY KEY ("Id");
 C   ALTER TABLE ONLY "dbo"."Suppliers" DROP CONSTRAINT "PK_Suppliers";
       dbo            postgres    false    223            �           2606    16638    UnitsOfMesure PK_UnitsOfMesure 
   CONSTRAINT     a   ALTER TABLE ONLY "dbo"."UnitsOfMesure"
    ADD CONSTRAINT "PK_UnitsOfMesure" PRIMARY KEY ("Id");
 K   ALTER TABLE ONLY "dbo"."UnitsOfMesure" DROP CONSTRAINT "PK_UnitsOfMesure";
       dbo            postgres    false    225            �           2606    16651    Use_Materials PK_Use_Materials 
   CONSTRAINT     a   ALTER TABLE ONLY "dbo"."Use_Materials"
    ADD CONSTRAINT "PK_Use_Materials" PRIMARY KEY ("Id");
 K   ALTER TABLE ONLY "dbo"."Use_Materials" DROP CONSTRAINT "PK_Use_Materials";
       dbo            postgres    false    227           