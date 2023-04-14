PGDMP     .                    {           StorageManagerDB    15.2    15.2 E    h           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            i           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            j           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            k           1262    16748    StorageManagerDB    DATABASE     �   CREATE DATABASE "StorageManagerDB" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Italian_Italy.1252';
 "   DROP DATABASE "StorageManagerDB";
                postgres    false            �            1259    16873    CategoryIngredientList    TABLE     �   CREATE TABLE public."CategoryIngredientList" (
    "EntryId" bigint NOT NULL,
    "Category_Id" bigint NOT NULL,
    "Ingredient_id" bigint NOT NULL,
    "Selected" boolean DEFAULT false NOT NULL,
    "Quantity" integer DEFAULT 1 NOT NULL
);
 ,   DROP TABLE public."CategoryIngredientList";
       public         heap    postgres    false            �            1259    16900 "   CategoryIngredientList_EntryId_seq    SEQUENCE     �   ALTER TABLE public."CategoryIngredientList" ALTER COLUMN "EntryId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."CategoryIngredientList_EntryId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    229            �            1259    16780    Ingredients    TABLE     �  CREATE TABLE public."Ingredients" (
    "Id" bigint NOT NULL,
    "Name" text NOT NULL,
    "Category" text NOT NULL,
    "IsUsedValue" bigint NOT NULL,
    "Notes" text,
    "QuantityNeeded" numeric NOT NULL,
    "ActualQuantity" numeric NOT NULL,
    "IsEnough" boolean GENERATED ALWAYS AS (
CASE
    WHEN ("QuantityNeeded" > "ActualQuantity") THEN false
    ELSE true
END) STORED NOT NULL
);
 !   DROP TABLE public."Ingredients";
       public         heap    postgres    false            �            1259    16815    Ingredients_Format    TABLE     �  CREATE TABLE public."Ingredients_Format" (
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
    "LastOrderDate" date,
    "IsDefault" boolean DEFAULT false NOT NULL
);
 (   DROP TABLE public."Ingredients_Format";
       public         heap    postgres    false            �            1259    16814    Ingredients_Format_Id_seq    SEQUENCE     �   ALTER TABLE public."Ingredients_Format" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Ingredients_Format_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    223            �            1259    16779    Ingredients_Id_seq    SEQUENCE     �   ALTER TABLE public."Ingredients" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Ingredients_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    219            �            1259    16755    IsUsedValue    TABLE     a   CREATE TABLE public."IsUsedValue" (
    "Id" bigint NOT NULL,
    "Description" text NOT NULL
);
 !   DROP TABLE public."IsUsedValue";
       public         heap    postgres    false            �            1259    16770    IsUsedValue_Id_seq    SEQUENCE     �   ALTER TABLE public."IsUsedValue" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."IsUsedValue_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    214            �            1259    16866    OrderCategory    TABLE     }   CREATE TABLE public."OrderCategory" (
    "Id" bigint NOT NULL,
    "Name" text NOT NULL,
    "Description" text NOT NULL
);
 #   DROP TABLE public."OrderCategory";
       public         heap    postgres    false            �            1259    16893    OrderCategory_Id_seq    SEQUENCE     �   ALTER TABLE public."OrderCategory" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."OrderCategory_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    228            �            1259    16839    Orders    TABLE     �   CREATE TABLE public."Orders" (
    "Id" bigint NOT NULL,
    "Supplier_Id" bigint NOT NULL,
    "OrderDate" date NOT NULL,
    "Time" timestamp without time zone NOT NULL
);
    DROP TABLE public."Orders";
       public         heap    postgres    false            �            1259    16850 
   OrdersList    TABLE     �   CREATE TABLE public."OrdersList" (
    "Entry_Id" bigint NOT NULL,
    "OrderCategory_Id" bigint NOT NULL,
    "Ingredient_Id" bigint NOT NULL,
    "Quantity" bigint DEFAULT 1 NOT NULL
);
     DROP TABLE public."OrdersList";
       public         heap    postgres    false            �            1259    16849    OrdersList_Entry_Id_seq    SEQUENCE     �   ALTER TABLE public."OrdersList" ALTER COLUMN "Entry_Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."OrdersList_Entry_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    227            �            1259    16838    Orders_Entry_Id_seq    SEQUENCE     �   ALTER TABLE public."Orders" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Orders_Entry_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    225            �            1259    16932    Product_Composition    TABLE     �   CREATE TABLE public."Product_Composition" (
    "Id" bigint NOT NULL,
    "Product_Id" bigint NOT NULL,
    "Ingredient_Id" bigint NOT NULL,
    "Quantity" double precision DEFAULT 1 NOT NULL,
    "Cost" double precision
);
 )   DROP TABLE public."Product_Composition";
       public         heap    postgres    false            �            1259    16931    Product_Composition_Id_seq    SEQUENCE     �   ALTER TABLE public."Product_Composition" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Product_Composition_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    235            �            1259    16923    Products    TABLE     �   CREATE TABLE public."Products" (
    "Id" bigint NOT NULL,
    "Product_Name" text NOT NULL,
    "Product_Price" double precision NOT NULL,
    "Product_Cost" double precision,
    "Product_Category" text NOT NULL
);
    DROP TABLE public."Products";
       public         heap    postgres    false            �            1259    16922    Products_Id_seq    SEQUENCE     �   ALTER TABLE public."Products" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Products_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    233            �            1259    16794 	   Suppliers    TABLE     �   CREATE TABLE public."Suppliers" (
    "Id" bigint NOT NULL,
    "Supplier_Name" text NOT NULL,
    "PT_IVA" text,
    "Phone" text,
    "Email" text,
    "Notes" text
);
    DROP TABLE public."Suppliers";
       public         heap    postgres    false            �            1259    16793    Suppliers_Id_seq    SEQUENCE     �   ALTER TABLE public."Suppliers" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Suppliers_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    221            �            1259    16772    UnitsOfMeasure    TABLE     d   CREATE TABLE public."UnitsOfMeasure" (
    "Id" bigint NOT NULL,
    "Description" text NOT NULL
);
 $   DROP TABLE public."UnitsOfMeasure";
       public         heap    postgres    false            �            1259    16771    UnitsOfMeasure_Id_seq    SEQUENCE     �   ALTER TABLE public."UnitsOfMeasure" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."UnitsOfMeasure_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    217            _          0    16873    CategoryIngredientList 
   TABLE DATA           u   COPY public."CategoryIngredientList" ("EntryId", "Category_Id", "Ingredient_id", "Selected", "Quantity") FROM stdin;
    public          postgres    false    229   �W       U          0    16780    Ingredients 
   TABLE DATA           }   COPY public."Ingredients" ("Id", "Name", "Category", "IsUsedValue", "Notes", "QuantityNeeded", "ActualQuantity") FROM stdin;
    public          postgres    false    219   X       Y          0    16815    Ingredients_Format 
   TABLE DATA           �   COPY public."Ingredients_Format" ("Id", "Ingredient_Id", "Supplier_Id", "Cost_€", "PastCost1", "PastCost2", "PastCost3", "Size_Kg", "Size_Unit", "LastOrderDate", "IsDefault") FROM stdin;
    public          postgres    false    223   �e       P          0    16755    IsUsedValue 
   TABLE DATA           <   COPY public."IsUsedValue" ("Id", "Description") FROM stdin;
    public          postgres    false    214   t       ^          0    16866    OrderCategory 
   TABLE DATA           F   COPY public."OrderCategory" ("Id", "Name", "Description") FROM stdin;
    public          postgres    false    228   �t       [          0    16839    Orders 
   TABLE DATA           L   COPY public."Orders" ("Id", "Supplier_Id", "OrderDate", "Time") FROM stdin;
    public          postgres    false    225   �t       ]          0    16850 
   OrdersList 
   TABLE DATA           c   COPY public."OrdersList" ("Entry_Id", "OrderCategory_Id", "Ingredient_Id", "Quantity") FROM stdin;
    public          postgres    false    227   �t       e          0    16932    Product_Composition 
   TABLE DATA           h   COPY public."Product_Composition" ("Id", "Product_Id", "Ingredient_Id", "Quantity", "Cost") FROM stdin;
    public          postgres    false    235   u       c          0    16923    Products 
   TABLE DATA           o   COPY public."Products" ("Id", "Product_Name", "Product_Price", "Product_Cost", "Product_Category") FROM stdin;
    public          postgres    false    233   .u       W          0    16794 	   Suppliers 
   TABLE DATA           a   COPY public."Suppliers" ("Id", "Supplier_Name", "PT_IVA", "Phone", "Email", "Notes") FROM stdin;
    public          postgres    false    221   vu       S          0    16772    UnitsOfMeasure 
   TABLE DATA           ?   COPY public."UnitsOfMeasure" ("Id", "Description") FROM stdin;
    public          postgres    false    217   �v       l           0    0 "   CategoryIngredientList_EntryId_seq    SEQUENCE SET     R   SELECT pg_catalog.setval('public."CategoryIngredientList_EntryId_seq"', 5, true);
          public          postgres    false    231            m           0    0    Ingredients_Format_Id_seq    SEQUENCE SET     K   SELECT pg_catalog.setval('public."Ingredients_Format_Id_seq"', 381, true);
          public          postgres    false    222            n           0    0    Ingredients_Id_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public."Ingredients_Id_seq"', 251, true);
          public          postgres    false    218            o           0    0    IsUsedValue_Id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public."IsUsedValue_Id_seq"', 8, true);
          public          postgres    false    215            p           0    0    OrderCategory_Id_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public."OrderCategory_Id_seq"', 3, true);
          public          postgres    false    230            q           0    0    OrdersList_Entry_Id_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public."OrdersList_Entry_Id_seq"', 1, false);
          public          postgres    false    226            r           0    0    Orders_Entry_Id_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public."Orders_Entry_Id_seq"', 1, false);
          public          postgres    false    224            s           0    0    Product_Composition_Id_seq    SEQUENCE SET     K   SELECT pg_catalog.setval('public."Product_Composition_Id_seq"', 1, false);
          public          postgres    false    234            t           0    0    Products_Id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public."Products_Id_seq"', 2, true);
          public          postgres    false    232            u           0    0    Suppliers_Id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public."Suppliers_Id_seq"', 29, true);
          public          postgres    false    220            v           0    0    UnitsOfMeasure_Id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public."UnitsOfMeasure_Id_seq"', 6, true);
          public          postgres    false    216            �           2606    16826 *   Ingredients_Format Ingredients_Format_pkey 
   CONSTRAINT     n   ALTER TABLE ONLY public."Ingredients_Format"
    ADD CONSTRAINT "Ingredients_Format_pkey" PRIMARY KEY ("Id");
 X   ALTER TABLE ONLY public."Ingredients_Format" DROP CONSTRAINT "Ingredients_Format_pkey";
       public            postgres    false    223            �           2606    16787    Ingredients Ingredients_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public."Ingredients"
    ADD CONSTRAINT "Ingredients_pkey" PRIMARY KEY ("Id");
 J   ALTER TABLE ONLY public."Ingredients" DROP CONSTRAINT "Ingredients_pkey";
       public            postgres    false    219            �           2606    16763    IsUsedValue IsUsedValue_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public."IsUsedValue"
    ADD CONSTRAINT "IsUsedValue_pkey" PRIMARY KEY ("Id");
 J   ALTER TABLE ONLY public."IsUsedValue" DROP CONSTRAINT "IsUsedValue_pkey";
       public            postgres    false    214            �           2606    16872     OrderCategory OrderCategory_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public."OrderCategory"
    ADD CONSTRAINT "OrderCategory_pkey" PRIMARY KEY ("Id");
 N   ALTER TABLE ONLY public."OrderCategory" DROP CONSTRAINT "OrderCategory_pkey";
       public            postgres    false    228            �           2606    16877 &   CategoryIngredientList PK_CategoryList 
   CONSTRAINT     o   ALTER TABLE ONLY public."CategoryIngredientList"
    ADD CONSTRAINT "PK_CategoryList" PRIMARY KEY ("EntryId");
 T   ALTER TABLE ONLY public."CategoryIngredientList" DROP CONSTRAINT "PK_CategoryList";
       public            postgres    false    229            �           2606    16937 ,   Product_Composition Product_Composition_pkey 
   CONSTRAINT     p   ALTER TABLE ONLY public."Product_Composition"
    ADD CONSTRAINT "Product_Composition_pkey" PRIMARY KEY ("Id");
 Z   ALTER TABLE ONLY public."Product_Composition" DROP CONSTRAINT "Product_Composition_pkey";
       public            postgres    false    235            �           2606    16929    Products Products_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public."Products"
    ADD CONSTRAINT "Products_pkey" PRIMARY KEY ("Id");
 D   ALTER TABLE ONLY public."Products" DROP CONSTRAINT "Products_pkey";
       public            postgres    false    233            �           2606    16800    Suppliers Suppliers_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public."Suppliers"
    ADD CONSTRAINT "Suppliers_pkey" PRIMARY KEY ("Id");
 F   ALTER TABLE ONLY public."Suppliers" DROP CONSTRAINT "Suppliers_pkey";
       public            postgres    false    221            �           2606    16778 "   UnitsOfMeasure UnitsOfMeasure_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY public."UnitsOfMeasure"
    ADD CONSTRAINT "UnitsOfMeasure_pkey" PRIMARY KEY ("Id");
 P   ALTER TABLE ONLY public."UnitsOfMeasure" DROP CONSTRAINT "UnitsOfMeasure_pkey";
       public            postgres    false    217            �           2606    16855    OrdersList pk_OrderList 
   CONSTRAINT     a   ALTER TABLE ONLY public."OrdersList"
    ADD CONSTRAINT "pk_OrderList" PRIMARY KEY ("Entry_Id");
 E   ALTER TABLE ONLY public."OrdersList" DROP CONSTRAINT "pk_OrderList";
       public            postgres    false    227            �           2606    16843    Orders pk_Orders 
   CONSTRAINT     T   ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "pk_Orders" PRIMARY KEY ("Id");
 >   ALTER TABLE ONLY public."Orders" DROP CONSTRAINT "pk_Orders";
       public            postgres    false    225            �           2606    16878 "   CategoryIngredientList fk_Category    FK CONSTRAINT     �   ALTER TABLE ONLY public."CategoryIngredientList"
    ADD CONSTRAINT "fk_Category" FOREIGN KEY ("Category_Id") REFERENCES public."OrderCategory"("Id");
 P   ALTER TABLE ONLY public."CategoryIngredientList" DROP CONSTRAINT "fk_Category";
       public          postgres    false    3249    228    229            �           2606    16888    OrdersList fk_Ingredient    FK CONSTRAINT     �   ALTER TABLE ONLY public."OrdersList"
    ADD CONSTRAINT "fk_Ingredient" FOREIGN KEY ("Ingredient_Id") REFERENCES public."Ingredients"("Id") NOT VALID;
 F   ALTER TABLE ONLY public."OrdersList" DROP CONSTRAINT "fk_Ingredient";
       public          postgres    false    227    219    3239            �           2606    16943 $   Product_Composition fk_Ingredient_Id    FK CONSTRAINT     �   ALTER TABLE ONLY public."Product_Composition"
    ADD CONSTRAINT "fk_Ingredient_Id" FOREIGN KEY ("Ingredient_Id") REFERENCES public."Ingredients"("Id");
 R   ALTER TABLE ONLY public."Product_Composition" DROP CONSTRAINT "fk_Ingredient_Id";
       public          postgres    false    235    3239    219            �           2606    16832 !   Ingredients_Format fk_Ingredients    FK CONSTRAINT     �   ALTER TABLE ONLY public."Ingredients_Format"
    ADD CONSTRAINT "fk_Ingredients" FOREIGN KEY ("Ingredient_Id") REFERENCES public."Ingredients"("Id");
 O   ALTER TABLE ONLY public."Ingredients_Format" DROP CONSTRAINT "fk_Ingredients";
       public          postgres    false    3239    219    223            �           2606    16883 %   CategoryIngredientList fk_Ingredients    FK CONSTRAINT     �   ALTER TABLE ONLY public."CategoryIngredientList"
    ADD CONSTRAINT "fk_Ingredients" FOREIGN KEY ("Ingredient_id") REFERENCES public."Ingredients"("Id");
 S   ALTER TABLE ONLY public."CategoryIngredientList" DROP CONSTRAINT "fk_Ingredients";
       public          postgres    false    3239    219    229            �           2606    16856    OrdersList fk_Order    FK CONSTRAINT     �   ALTER TABLE ONLY public."OrdersList"
    ADD CONSTRAINT "fk_Order" FOREIGN KEY ("OrderCategory_Id") REFERENCES public."Orders"("Id");
 A   ALTER TABLE ONLY public."OrdersList" DROP CONSTRAINT "fk_Order";
       public          postgres    false    227    225    3245            �           2606    16938 !   Product_Composition fk_Product_Id    FK CONSTRAINT     �   ALTER TABLE ONLY public."Product_Composition"
    ADD CONSTRAINT "fk_Product_Id" FOREIGN KEY ("Product_Id") REFERENCES public."Products"("Id");
 O   ALTER TABLE ONLY public."Product_Composition" DROP CONSTRAINT "fk_Product_Id";
       public          postgres    false    235    233    3253            �           2606    16827    Ingredients_Format fk_Supplier    FK CONSTRAINT     �   ALTER TABLE ONLY public."Ingredients_Format"
    ADD CONSTRAINT "fk_Supplier" FOREIGN KEY ("Supplier_Id") REFERENCES public."Suppliers"("Id");
 L   ALTER TABLE ONLY public."Ingredients_Format" DROP CONSTRAINT "fk_Supplier";
       public          postgres    false    3241    221    223            �           2606    16844    Orders fk_Suppliers    FK CONSTRAINT     �   ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "fk_Suppliers" FOREIGN KEY ("Supplier_Id") REFERENCES public."Suppliers"("Id");
 A   ALTER TABLE ONLY public."Orders" DROP CONSTRAINT "fk_Suppliers";
       public          postgres    false    221    3241    225            �           2606    16788    Ingredients fk_UsedValue    FK CONSTRAINT     �   ALTER TABLE ONLY public."Ingredients"
    ADD CONSTRAINT "fk_UsedValue" FOREIGN KEY ("IsUsedValue") REFERENCES public."IsUsedValue"("Id") ON UPDATE RESTRICT ON DELETE CASCADE;
 F   ALTER TABLE ONLY public."Ingredients" DROP CONSTRAINT "fk_UsedValue";
       public          postgres    false    214    3235    219            _   ;   x�3�4�4�L�4�2�4�413M�LCc0�Ȇ� Y�%@�!H��4����� d�	�      U   �  x��ZK�$�]眂{l�f�d�7XY�j��������7�% ���FK��+_�7�I���d2?=K*#���xlem�H�ݩ����x��{�F�Vu�\��j�~S���\.&�kuJ�l��Y�I@:�
'pڑIRv{bI��-�즞�̴��js��������I]V����_�OR<�n��iq!�~du;�$q�޼���V�I�>Z�����Q���?~v�DtTmsֵ��U��Ͽ�����}���Ͽ�����������?����#��&����g��'��S�DG�2�dnѝi��|,�ɋ���b=u�nc8�pյĉk�6$���LB��dI^:#M�jXwS�Þ�y�Z��z���̮XA��?�K$o
�Џ���3lht/[m����nK����k��Y����fړnds2�b�$+⨓yUE��׿Yr('�a�F�T�0}3ݼjhs'�q�9q�����	6�nB���䃊N�R{s�L��������)6u��/�\dX�{;݆�x�A�4���d�V��lg=ET�۟XY�aM��Ss���e��C�<�����~���;��r_E�T����+�p.��ŔF�tCZ���ОU냕R����U�i��Z���n+��p�UKn��|�N�757[���ף�@�I0��8�6Dp��I��H�D�Cy�1u�E��!e�!���s��z�aZe��͛^7z�	�` (����o���U�4�ԌNL��޲Gy�f� ����K}#���D$���q0�9�	�g�W�:�=ҧqԛ�1���C&����K[ӥ�L��d�e8�&ύ=���Ԫ�D��ӫ�H�/�����ѷ�lo^��K*J�s�@*�i|�%0��-ؘ�^���U��V�Ay�n,J[܋`��J��|fq�4з[c��lp���z�'���v�H����'���bQh* x������N�).[֑�}�BL�t�Ŀ�w{���ѝ&�ZU��5���&�b��BdIιɮW���KHw��D9:~"�6v������3�5(�Ɲ,JZ$D��'�|bح�:/��g���9w��2�v��1|.���u��=�$��w�p���w�Y�(	����!q��g�m ���=�ZKGΦ"��s���[���h�ݥ2A�<��p=�
U�����<mMQģ�A�$���ӓ�&���e��������N�W�	Sl��������1(KT�Q���U�x,�H7DDPv��Zr��E�oD�\��)ځ�ڎ�
p�N#1C3-8qQD�A�>�~DJ�SΨL_!�}��\C�l��|K*�T.\�Ut6��4O���q����ҳ��ǒ�("��7�k�~��P�R�.��Э������>R�����$*ӵV��X&��ן�we���j�Vg�'f?~z�������mˉ�!"�T��K'��H�!���H�}��<�-A���eQZ���,�r_g2T���$ؤG1���P�Z^�[a��M��ǋ�MUh�%P���8zTg�����C��u�� n�e��x[�''#>#n� *}�ļ�&h�c�5}�j�5�忼����C�5U����$f�����0�T_�7��7 `�D�G������]�E����R����KR��8�u:K����$[��Cpr� �qJ����tj+ģ�l_hc1.�����/�%�ՠ"�#Q�o�:+�����[�'9ĕ��e��wr����^����?�`t多'�FO@O�-�(�(��'4�sN9{��KVN?u�h� �{��
gW����t'E��f��T�\ku2�����W�"����~퓗��sxQ��6Fx:.`G?��Q��E2�%W"�v�K޵qx��;�#�I��`�+;$�̋�6��~aK9�
]���6Y�hE����g�)�P1�����z�F��ݱ�m���1����"��7�H8���W;5�� ��Q�Wxb�8d��C��*�Ѕ �Z�(k����"
N��\�D0O�ۊ�t��F<d\��,��a�c�))�*���;�y�~a�"HJ�X6&���5�<!($�����ѥ�[���9D/�l��ӛ!0M��%�ԇ�j��x3��IF�A�U�n�"��^�f��P�����J�'Ɏǯ���x��neg.[�͓*�Æ�eG���k���O��k�A
� K�'�d�'/�����{�3;��jK&�N5 
��ԭ�lq�o��y~�ǻr^/���� �����y0���z�;a�0�����5�`�٣sI3�y��+�S���?_Y1D�����iW���ȳp;�9�AS���䚏�4���.&��X�v����[��P�䡰s��^ȍ�,ꆳHW��v1��2�e�8�KFͳ��vC�F����6���T<�(�q��a!;�#�l�"�s2p�������\�����Oo�����Oü�T�?K�[�2�{g����OǺ��8�g�f��Uզ�u�˚ꥊY��T�^�I�����\u���r/;9vćj�@�O%P.C�<4��2��'���<hv��4�ڄ��ýp�"�d*��T���zn9+��]�������f�x��A��,U��\DaQDA�2���+����H��d�KLu�QC�j4yQM7K��%���7Ck'��`~�B�h�Ց�(��?�V��A��ٵ��,�EwCs��W�{�?��=7��S\"H�ɧV	/�_�c��t��.�ǎ���׷�&��������gtz�iYN��.�:��0/�Ij%=[��B��+��u<ZZ��Y��|�m�/���^��a��x{�іvi�f���b�U2�j����ODkΆ�����~M�"�TI��$h�m��P�w�h4���������ߡ<�ŋ�=��@�m���b܉!v�Q���"v�ib��.٧��j�����v��Q�ݭ��ԋ�cU�nMh�j?:�fUw��I�f5���<�$���E��V�+�~{Yt�[�YY_�3�_���3����GhK�V�%�pw������a��C����@<����|���;��mC�D ��ܯ�p�Zv��c��yb57 =�_v�Z��j��w`=�n�.����{�;rJk=�����@m{@B~�k��/RB�y�m��v��E���h�=�s��^�h��h��殕�D;U���mz��jGØq�!�|��q�����Q��UE�<R��	4�nv�8�]��&�3�Y�G�>��K϶�O��D����!����s�n`���Om�G7��L�i��%�f;��C�\��O2?��])l�r���'mF���wʈ�*�s�}�ι�W�Vgg��32�L<�T�S��o�{0V�����|���r~HښM5���~x{��P�;6-��D'�Ӌ�A��7t�K�FmNg�Q��v)8º@����k�:m�"M��Y~eG�NM����k����1�ܵG|�Ͱ�ay����߾z���s��      Y     x��ZI�%']g&�%|�Z�}���)���W�n7*��+_�
���?������O�k���m�R��?4Mh�NEA)�ME��ſ_�'.�����
s'F��w�y��t�xW��$>�O��y�:��R�D���D%2�z��$|Ϊ������LC7��JUn�cR1��l�O.�oz(�������M+n�"�nLt���3g�����".8d�'5>����X{HII�5FQX�����B��]a�jJ�
�i�Z�a(��M �}ìc�̑����+A-b�Y=���!JW���+L�zH�wZ�5R�Ca�����R��͌X;ݖ� �0��R�Xnf�e�:��{S�^W��n�*��5ҳFave���F��
LU"CQ�c*�"�,�5*,�<�/,��7��|A�O*� �r���������oӬ�A�
;	�&H'������%�.���<5\�.U,��iVG���q�˕U�lI��x�K��an%�*�}�KD|X����(�����gߔs⣻�>s��K*.����!s5�_pl��8��?��2��P�Mã$���4Ĭ�2����3Ȱ��$)	�3Nqs͍������A���+�1W�����K~��u0N1S��_#�װ�D;8�5�0���P��#�F����k-M�z�d���[�1H�G_b�|���Y��C�5.���-C�͞p}ƃ�~��D��nWX����xoК�����+A8\�+7�3a}����6@">��Tq9�`�H�ыw�옎%˩�⦦ZJϊ����@�S^l`��U=3���,�*�Cf6Չ/��Y��b��5��Y���g���ŏ�p����s��C�����=�U�m�{�K��&�C�5��Q�v��U\���J��I>).�<"e�,����s.���퐇���(��v��s<Ȁ5�B�*�&x�6�z����$���06Ŋ�	������G$���)�N*0�8}�P�7S���o�M`�.��|�栘s�L��$��8���*�t�Ya�T�a�.i�@`�#���z^���s���:�oV�D0VYh^47�AE;�zI��i���t2��K�z,bD5�UKVxYz��Őp^S�`�ꒋ�6�V��j�9Tu��\�`M����q�F�O"���^5�`.(�皢��1M�3�_d�bt4��\��.�:�٬z#�/e��!�.Ue��m�Ą�dl	ƛ�
��npL�V�=��Rp)/�
⹃yn�>�@�M��.$��)�sF���>o��xE��<��&��6㨘�]�Z�
��9GVB�O� ��@�ç�� 5s�N����q������th�$�oN��7����-u�Ǡ��H����#�Ir\$�A���������[	Ca��
ə�lUN�E����b��*��TMn�®��B��ء�ow�����rVX^#�B���m�w#*��aF�τݭ;�j:H�'X�'�����z�	��v|�b�y��#��z�u$;9'd��8q��9�͢4��F�.B�Z�����Ŋv��׶͌88b�t"�dڡ�wSX��@ૈ2��R\Mn�u�V�(l"��BR�}�J��)w�������)A�FV�:9�YH��,
��p�&��+),���fBL�c����t-}�l��1�l�$$k�� ��p7ܦ�M��!nI�Q�7�ij���+=���2�ˈ|)ͺ�,܈�Y�������#^��v	hp��=��6���L�¶��agk'��"��$�RG��y�%��O�T��W���� <��!��a��L�ِ�xq��y�_z�>	A�>6>i�劇<u��*�<a�͕ ����X�bU�����]s3B�������QM�1�7�H��H4?����HpB�A��tss$]	m�>�e"		�t�8u�ێ�m**Xb�;��y�a7%���wW�?x.MOu��n�s0|�E��5MuT�|����JW���r�T�kU�=��v�dH����H���}�g� w���ӹ��vG�Q��A��}���I(�t"܋	l�<�s�/=�5.��W�\;�H�8���)U�eT��J�JU���BT��թ�'�Wm6�\�%��`���-Z~̄<n��Y�0T?�9���vJr�(Z_��.�c�{pd�G/� yť^nW��ȹ.�_"��d�t���!%���u�>�]���M�B:��F�y�C<�!��/�ݠ�%��P�jD�?m�l}��$��<J����oI��K+�B����L�ٍKZ �<ޛf.j��Hwr.
�y^$�:q[,+M�#w0;
.i��^��c?n5�^�t���[�Ď7�/v���g�n��=�;�	��p�]��ݖPhI���|K~���H��j!?T�:��::�,VO�����-�a�pQ.�x��9�4����lM�Y�$��*M��[!Qc��b���X��w	������={<(>{M��q�]ay���+���ȜfWin���=�(��F>`�7���I
T]"�?Țv�#R�x����!�xY)�\ԗ�^��>PYIK�ߜ$i�.
�,�מ���c6���]�qs��}h
���=r�2E���5��=Y�T4��F��U�w�o��E{J��/��~(J8Y��Paq2�&8ґ�wKe�������%vnR�ང���<T0I:TH	�hC\.����;lr|i%TY�˓��K&���w80V�b�����j�ev"��ڗMD�Ua��j�	i��#"!B�4�
�>��1\fU��EM�J��W[An��0���x�D	ՖPud����})�E']��|�S�N��DY��J{̄BK��P;��>8�-�����gi}7<�Q|�W%�G'0�M�[����g�"�6NA�dU�&b�JI��������2
�����'B]��!�"�c�u�<��rI�U��u%�%�ÿ�7�"�j�[�"P�-b��&��ѻme����S�JE����VQ��H��J�e-��F�R�y���jcvSʍkx��z�>��z+8ae�������˴����;�ALR8��ܕ`��%N\d���2	+���۰��1�2-�{����b��&3�HZ]~>�t$h�h�4G�u@��s��#No�w��j�K?��rH��:\k�C�:;��8ۿ�Rf���{
LT�o���n�R��H.��?�qb�CO��)�Jץ����ld%yM�������F���]�E��"y�x����?���SĆ ��TG��H8x\�7�T����~?D0�ʸ��f�� E��=�5f�x
����8�.����V.�L�-�*L�li��*��˒��s}���W��އ+`��\�
;��>������R7r/��L_g��Uc�p:JS��W��?}�L��"�G�&��)��f.��z#9��Ef�����:�1"#b}ΤD����l� �ߜ�e� ���Y�|���s��9���`-L��VF�[�i�%b`����a�-���.$�t.�����O�޳u{��{�����|�O      P   _   x��1�0���9ZV������2��� �7����&H4�n_�Z#�H;�˲b"[���?�����(s:x�{�|U�\̳���	>P      ^   B   x�3�I-.�tN,IM�/�LT�/J��KU(H-R(�p�(@)0QB5��b�1��v��qqq ��7      [      x������ � �      ]      x������ � �      e      x������ � �      c   8   x�3�(��+���I���44�3���t�L��2�t�,*J��8��r��qqq ��      W      x����n� ���vZԐ��G'q�5�J�� �&�ǽ�X/�4ɜ����ߴ`j�n�������u�w����G)x����+�����i���pq�����EtL�(�L~Y����d���#�F�E5њZR3Ye�����,�Q6�0$��C%���p5
z�)�g�#�Ҍ{@�W��� S��:c��ka���[z����U�z���cn�¥g�{��bp���M�܇:1�$��A �1Ȫ���-�֣�"#�m5��a��n�~�������o��Pl      S      x�3��	�2��v�2�������� .��     