PGDMP                         {            StorageManagerDB    15.2    15.2      '           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            (           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            )           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            *           1262    16748    StorageManagerDB    DATABASE     �   CREATE DATABASE "StorageManagerDB" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Italian_Italy.1252';
 "   DROP DATABASE "StorageManagerDB";
                postgres    false            �            1259    16780    Ingredients    TABLE     �  CREATE TABLE public."Ingredients" (
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
            public          postgres    false    214            �            1259    16794 	   Suppliers    TABLE     �   CREATE TABLE public."Suppliers" (
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
            public          postgres    false    217                       0    16780    Ingredients 
   TABLE DATA           }   COPY public."Ingredients" ("Id", "Name", "Category", "IsUsedValue", "Notes", "QuantityNeeded", "ActualQuantity") FROM stdin;
    public          postgres    false    219   Z(       $          0    16815    Ingredients_Format 
   TABLE DATA           �   COPY public."Ingredients_Format" ("Id", "Ingredient_Id", "Supplier_Id", "Cost_€", "PastCost1", "PastCost2", "PastCost3", "Size_Kg", "Size_Unit", "LastOrderDate", "IsDefault") FROM stdin;
    public          postgres    false    223   .6                 0    16755    IsUsedValue 
   TABLE DATA           <   COPY public."IsUsedValue" ("Id", "Description") FROM stdin;
    public          postgres    false    214   ID       "          0    16794 	   Suppliers 
   TABLE DATA           a   COPY public."Suppliers" ("Id", "Supplier_Name", "PT_IVA", "Phone", "Email", "Notes") FROM stdin;
    public          postgres    false    221   �D                 0    16772    UnitsOfMeasure 
   TABLE DATA           ?   COPY public."UnitsOfMeasure" ("Id", "Description") FROM stdin;
    public          postgres    false    217   �E       +           0    0    Ingredients_Format_Id_seq    SEQUENCE SET     K   SELECT pg_catalog.setval('public."Ingredients_Format_Id_seq"', 369, true);
          public          postgres    false    222            ,           0    0    Ingredients_Id_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public."Ingredients_Id_seq"', 247, true);
          public          postgres    false    218            -           0    0    IsUsedValue_Id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public."IsUsedValue_Id_seq"', 8, true);
          public          postgres    false    215            .           0    0    Suppliers_Id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public."Suppliers_Id_seq"', 26, true);
          public          postgres    false    220            /           0    0    UnitsOfMeasure_Id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public."UnitsOfMeasure_Id_seq"', 6, true);
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
       public            postgres    false    214            �           2606    16800    Suppliers Suppliers_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public."Suppliers"
    ADD CONSTRAINT "Suppliers_pkey" PRIMARY KEY ("Id");
 F   ALTER TABLE ONLY public."Suppliers" DROP CONSTRAINT "Suppliers_pkey";
       public            postgres    false    221            �           2606    16778 "   UnitsOfMeasure UnitsOfMeasure_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY public."UnitsOfMeasure"
    ADD CONSTRAINT "UnitsOfMeasure_pkey" PRIMARY KEY ("Id");
 P   ALTER TABLE ONLY public."UnitsOfMeasure" DROP CONSTRAINT "UnitsOfMeasure_pkey";
       public            postgres    false    217            �           2606    16832 !   Ingredients_Format fk_Ingredients    FK CONSTRAINT     �   ALTER TABLE ONLY public."Ingredients_Format"
    ADD CONSTRAINT "fk_Ingredients" FOREIGN KEY ("Ingredient_Id") REFERENCES public."Ingredients"("Id");
 O   ALTER TABLE ONLY public."Ingredients_Format" DROP CONSTRAINT "fk_Ingredients";
       public          postgres    false    223    3205    219            �           2606    16827    Ingredients_Format fk_Supplier    FK CONSTRAINT     �   ALTER TABLE ONLY public."Ingredients_Format"
    ADD CONSTRAINT "fk_Supplier" FOREIGN KEY ("Supplier_Id") REFERENCES public."Suppliers"("Id");
 L   ALTER TABLE ONLY public."Ingredients_Format" DROP CONSTRAINT "fk_Supplier";
       public          postgres    false    3207    223    221            �           2606    16788    Ingredients fk_UsedValue    FK CONSTRAINT     �   ALTER TABLE ONLY public."Ingredients"
    ADD CONSTRAINT "fk_UsedValue" FOREIGN KEY ("IsUsedValue") REFERENCES public."IsUsedValue"("Id") ON UPDATE RESTRICT ON DELETE CASCADE;
 F   ALTER TABLE ONLY public."Ingredients" DROP CONSTRAINT "fk_UsedValue";
       public          postgres    false    3201    219    214                �  x��ZK�$�]眂{l�f�d�7XY�j��������7�% ���FK��+_�7�I���d2?=K*FD��/�-���Mɻ;�����"����H֪7�٣nL��K���4:����"���H^�"훹\Lt��>��#٨�E�Z��t�	N�#����Ē��[&�M==�I��js��n������I]V����_�OR<�n��iq!�~du;�$q�޼���V�N�>:�y�Ȍ
̉(M�ϟ??;%U�Ýu�gg�1���o����b�����o�?~xƿ���#������>����Iƾ����*|tf��S���Ew����9��(�Z����Uj��a��U�;�eې|��3	�Ғ%Ix�,��a�M�{��ytkMW�6D~3�beV��.��)8B?Z��ΰ�ѽl�a�ӷ�:�ra�ٷ\��V�:s1̴'���d��IVĔ��������P&N��A�:�i�f:y=԰�v�"s�b%9�
	n�)��\�AE's��;n�}Ҵ���c��J�j�1���ζap^gpq�ތ�v�yT��I���"���O�,Ǵ�\����n�2��!n������w?����}�W���pPv�f�%�����фO1�u���;�Y�>Y�j�[y�j����V�&��S夊 \��k\K�{S�"�d���z��0	&��mnS����a���aO�=t�6�N�h{s�XzH�1�Ü�U=c3�2L��M���Q0 �@���o���U�4�ԌAL��޲Gyp�!�d�\�a}�$"��Ϗ�íu��zvy�o�G�4�z�4fst~Ȅ���[9`i�T �I�B@�[F�n��ح�K H��J�"�.t�/������ѷ��o^��K*J�s�@*�i|m%p��-���Q���U��V�Iy�n,J[��`��J��|fq�4з[[Ρ��]�׃>������:!3� ALh��$��T@�^����ѝ<S^4���z�A�
19҉g�
���]�,��4�ԚBŭ�ׯ7� + fZ+DN����z�Y��tw��H����'��I�CU4�ş�,�AY5j.�(h��ޞ�ᷚ�\$c������9����[�e@�wj���?v�m+�����Y�%���Mg)�$�^�.�H�C�<�n��D��a�Z:r6)'��u �z���(�C��.��z��qW�
��[��#���E<_4K��M?=Im¬(�;�<<д�
1a��y��]m1
����DU{Z���"�tCDe'8���!w��H#���8E;PU�qZ.�i\��MN\r�����Ԁ3*��W�t?�"א0O>���3��yE�M{6͓�&he�3sA��,���6��i����_�@���k)�t��y�VL�����>R�����KT�k�L��L���~�����jp��*�O�~�����3���Sˉ�!#�T��C'��&�-a�ͽ^��]V]��	�˲?0@�uf;el�N�.zH#M)�+�e��c��3�v<�S�£[���$TŁУ:��X]��bǟ+��nK4���9y��Q�D�n�M5A��EE��7�&[����?>�mQ�x��ͫ��>R5��fу�����k�@�I"�#��~��ҕ�]�E�I�h�KK��%��J��:��c���.�$[��C�s��@�qF@���tj+ģ�l_��b1.b۲����$�@~;2��T����*	Ϻ�y�C\y�,0f	�p'�f�!�^%�f_tX5���q���#'�<I6Fv�l�%�Dm �<��3p���j>d��Sk�^���a�p&pp�oϖЈ;)j.�5#ϥK�X��Y���?"Q׷��똼ޜ#�Z]�9��q�pTTwF�b��x\��+�n.��r*�-��o��4�@�
����2/�j����J9�
m�
���zъDu��ш,RAĔ�S�"�����GTU�B=���,�GK�0��0b��0�^�e���P�Wxb�8d�ס�Ѝm���0O-Otk����"
v��\�D00�jE�W:Zx"�.�P���0*��Tq�?{�j֠�/�\��f�nI�{�)O
�9!p�p$b��w����J�B��M���ޙ��$������e����$�Р����5�S�^��k�z�	�7n�
��d�����r��Y��3�-��I����ײ#��ȵ������� 	w�%�Er��t@�؋��ڽ陝9m�%�H���z��G:8�7R�<���[9N���w��mo�~�i����9��hhh��+XFv�(ǻ�K3N�y��+�c���?_Y1d����I+�H\�Y8j���|�AS���h���4���.F��X�v�����*�V%.y(�9�r�,���l�	���fLfc�J�0MF���%��Y��p��V#���q{���L<�)dq��a!;�3�l���srp�������B�����Oo�����OӺ���'~X��x~�F�;�;��~�7�-�š<�,�	�����6����\�T/U�R���� \
X�v1<G�CV斋x��Ǝ�P�"���e�S���Y�X��(�"�g�N��&0��Y q�NX��Rew��3�m䖓�"��Nɾl�l����[�����x�EE4*󳊾"��z���N��T7�5D�F��$q��q�\"@-��J}3�v�8O޷&���]	�J�co5_��͞]���2Yt74��}���s��#;�%�Ԝ���"!��|pL㜎�c�E���%�8����	��d�~������znZ��g��������j��YI�P!��b��[K�y��Q���������+>��1�o/<&��.��K��\l2�J&^�Ѓ��h����_�uӯ�P$�*	���d��Mj�N��_1۾���;t�Bd��~�i �3G�if�wb��qֺڶ�c���Ƅ�K�mp��������md�yw�cǇ<�"�Xջ�@��������F�bj���L�ë��-���pQy��q�B�o�N|k\7+��a���w�<����m��
�n��u@q���=�tt���V�ȧ3U�U�yz'�r�m� ���b�6Qˮ��1� /B��䡗��.R4�@u����-�����?@��@������.�ж$�?7�O���$���s5� >0�%��u+�:{&���b/�F��@��]/��v�v����>EՎ�1�pCD��'����Q��6UE�<R��I4�nv���]��&A0�Y�G�>����eǟ$K��ۿ�B0��Ѓ;u7�p~�6����l��4�p�.�f;��C�\��O2?��])l�r��mF���wʈ�*�s�c�ι�W�Vgg��;1�L<�T�[����H{0V�����|���r~)ںM5��?tY�,�=yD�����b��m�� ������c�5g�ژL}�la]���Kݵ�{~6{���z���m�&�fv��."{.w-�_3�|�dXn8~����^��?�      $     x��ZY�%)���Z� �M�
������ZG���wY���*�%(W����������ԫ\��� ��p��z�����<T�|�^�����e(��c��v����W��57=t��=�D�Iwh��Н��>K��#֛WT\4�B3�1�^�"�0���άO��Մ�]a[9��6��m�O,V�������#�����2Q���^13	3�*��j}h�M�,tS�8O{c���_e��6�}�CI<�pe=oZqc61u���zҙ3���ŉ��(8d�'5>�����X{HI�Ȯ(�Y�CRu�~箰������f���zJ��SHa�0+h<s$�f���x%�E,7���Z�=D�J"�pw��^��N�F�w�Ca��1��R��͌X;ݖ� �0��R�Xnf�%;�5�h�vl��4��U0�+<k�g����V�G9����V"CQ�c*�"�,�5*,�<�/,��o�)��F�T��At�nYacac/b/$ߦY�-��vvM�Nw+�;:K�]$��yj�f]�Xf5Ӭ�(*��ė+����v���$���JJU
��KD|X����(�����gߔs⣻�>s� �K*.�iA�� �1���p���q������-J��Q��^ubVxqc	W�d�L�F���͏����FJ�`�y� υ�����]����%?[��:���*S��kX������X�'g-
�=F��X1i�T�%����S�d��1�N�g�O_J�H������=\�uGw�9�a:>"�����ǖ,.?���B�â�t�~�KUE4�=K�
,6sW'�쨳��c��5��Y���B?�õ�z���J����W���	/yM�x^�k�����
w����9'rl�G"�Iq��)�(	iV�ʘ��!ͱBQ��`�k��0�
jځlMpM�/��X��ϒ���CaS�� 
�hxD[a��������C��L)�`>J�X����*��Z#s���IBtr�ډ-V���
��t��,�m7<�p�v�y1�P�ρ�s,��YQ�lXe�y���]�H�%���oO�yU9 ��z�5#�M߳���#{�.����#hW��EJ�J�Uۘ�>��m��e_Z-0���@x0|��~8u�U�+sAq?<'�]|�Ni�I<�"K{���Dδ�؏v�֩�Te�A}i(_n�h��#�n�$&��X� `K0��	Gv�c�������+�"� �;���E8�x�|��p.< �O���0�t�yC��+��>�h�ڌ�b6w�j=+���du@X	Ya����*~�����)�X�vX&p�#�M�HA��Q${|�pھD�:ƶ���#�p�W��&�q��Q�_�:"^/h�!�m�*$g�f�U�9Mx�~N�sR8x��S5�
� �c7�Cɰ;j��
A#W5(䬰�F6�,-��\'�FT�Ì����[v*ee�H�H��M'��<*��u|�b�y��#�z���HvrN�$�q��s��E�����jq��o�+���T[�63����Ӊ��i���Ma�'���"ʀ�Cqm4�] �R����-�D8*I��+����Y����0r��\��2q��QE1^4
_U[�4M��WRX
C�̈́�D��ۘ�t-��l��1�l�$$k�� ��p7ܦ�M��!nI�Q�g8���~P��[Wz����p�p#_J��)7"pV��#+l$h���]\�~���͢<���-�%{��Zĉ�îHb"ɷ�ߑtq�g�q�%Ub���?�(���:�0�i=2�0 /88����������Ca#ა�!�x�SX�r��)�e!�)��A��AT�]]]��u�"$,-�(��|���4�8q���y�D�c��'��M77GҕЏ�_&��HۉSG���ܶ���%Fi��ߖ�] �g���fL�l�t�P�M�����!�9:�4�Q�fG�&*]�����IR	(�Um�P�2{��N<�!ͣs#!`�����A�2
�sw�6�£j������%X��P��D� �\y��,=�5.��W�\_�ݏ1����b1��*؉�
BIb[�
�Ҁ^�*Ը:]�D|���z���Ys��ź�ҟ����<6+fBt�]$t.R�{RIn��$
:��ڔw��m`4�Bq鞻i��ņ��(��[P�˷���ٻv��ڬ���!M*Ju��2�y!<�!n�c��o��Q�(N�6"�v}��^tr���C�|����S!�jwhf)$��g
�M���y_�;9��>/cJ��-���ȑ�;����{x/M�.�ܶ�6n~��?����'�n��=曛	��p������P�I�ę�|K~���Ȱ�w��Kwכ��J	PQ++������~�{�� \������r�d	W��綦�d�Ca�f�C᭐(��t@CaMDO�,��v����E��]u<(>�T�q�]a����-���ȜfW��b�=^(��F*a������I�T]�Ț6�#��xH;�!�xY�s#\v��6��>�A���/]��gX��kϪ�:��}�4qW}�~iU���$q�����uws�> M�`�Ѽ��dU��]�['oFў*����
��Tf#���O(θ8�Z�H���2�mtpn��	�5�;7)z��@a�p��$����!.JW|�އ�
69>�M	��x��P��DQ^H�	G��c�"�ځ���hMtc�N$EZ���@D�ZPvMa٠�@��&<�0""dR�(����g/?�e�	�d]�_{E��V�k�����K�P�	UG�,p8�Ȑrp\t���W���wR� a�
�Vڃ �h����������Ш���RO>K׼���˿�(�?��y類\X|
����P8m`�Ua���+%x$yLA�hp�"���E(�>��,�%n�|�Ď�&�p.��Rt�L�x�$��>//wK�}w?,E�����o���me����S�JE����VQ��<��J�eݓ�F�R�y���jc6bʍ|��z�>�e�zo7ae���/<�;�?q���%0w裒�p���+���K���P�=�eV���wp�%c�E\�7ww��.*Mfxj����}j�H�;��k�
�n)V���W#No��[JB���J%t+R=$��7������ǎl{���U��sO�������w'��^ʕɝ��N,x,I"��4EQ�t�ݘ=�$/�~ǹ}�t���\����h�Z$�(Ϣ�;ڵ��CJy��`����,	�����TJ�4�{��o�HxW%�IW��,U��^�g�F�O��Bu�G���Tޛ�i�Y�I�-M�]�tY�Q4��<��?���az��p�V�k]agt!ѧ��0y>�A�F�U�1��s��^�	��4%
x9��A�t	�U9p�l"��½�.�w�#N\d�i���H@��#2"��L�Ȳ+��z�6�����8,K�<��͚H�l��'�΁���ha��g6һ�*N{K,��]���/�����=f�����������>         _   x��1�0���9ZV������2��� �7����&H4�n_�Z#�H;�˲b"[���?�����(s:x�{�|U�\̳���	>P      "     x���;n�0��>��N5b�=�6�����$�ZH�^�j�Ԥ�H�TÈ�ܿ���|6(%���t�ך�~	<����A�,Q5{�W���6��-]Ux���#��dv��΀�WE��!N��D��n;�'�X�h���'3�7nIY��ۂ�U_Y��J�Ta{(�$6�Ύ��%����'��xx���N0�)Q͊B����gA��,aG���;�H�L��9�{��3m���Q�.m}v��\q���݁�t��T���l{}Em�4��TQ�            x�3��	�2��v�2�������� .��     