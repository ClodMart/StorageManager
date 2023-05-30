PGDMP                         {           StorageManagerDB    15.2    15.2     k           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            l           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            m           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            n           1262    16748    StorageManagerDB    DATABASE     �   CREATE DATABASE "StorageManagerDB" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Italian_Italy.1252';
 "   DROP DATABASE "StorageManagerDB";
                postgres    false            b          0    16873    CategoryIngredientList 
   TABLE DATA           �   COPY public."CategoryIngredientList" ("EntryId", "Category_Id", "Ingredient_id", "Selected", "Quantity", "SelectedFormat_Id") FROM stdin;
    public          postgres    false    229   �       X          0    16780    Ingredients 
   TABLE DATA           }   COPY public."Ingredients" ("Id", "Name", "Category", "IsUsedValue", "Notes", "QuantityNeeded", "ActualQuantity") FROM stdin;
    public          postgres    false    219          \          0    16815    Ingredients_Format 
   TABLE DATA           �   COPY public."Ingredients_Format" ("Id", "Ingredient_Id", "Supplier_Id", "Cost_€", "PastCost1", "PastCost2", "PastCost3", "Size_Kg", "Size_Unit", "LastOrderDate", "IsDefault", "LastPriceChange") FROM stdin;
    public          postgres    false    223   �%       S          0    16755    IsUsedValue 
   TABLE DATA           Q   COPY public."IsUsedValue" ("Id", "Description", "CorrespondsToUsed") FROM stdin;
    public          postgres    false    214   :4       a          0    16866    OrderCategory 
   TABLE DATA           F   COPY public."OrderCategory" ("Id", "Name", "Description") FROM stdin;
    public          postgres    false    228   �4       ^          0    16839    Orders 
   TABLE DATA           L   COPY public."Orders" ("Id", "Supplier_Id", "OrderDate", "Time") FROM stdin;
    public          postgres    false    225   5       `          0    16850 
   OrdersList 
   TABLE DATA           c   COPY public."OrdersList" ("Entry_Id", "OrderCategory_Id", "Ingredient_Id", "Quantity") FROM stdin;
    public          postgres    false    227   "5       h          0    16932    Product_Composition 
   TABLE DATA           h   COPY public."Product_Composition" ("Id", "Product_Id", "Ingredient_Id", "Quantity", "Cost") FROM stdin;
    public          postgres    false    235   ?5       f          0    16923    Products 
   TABLE DATA           x   COPY public."Products" ("Id", "Product_Name", "Product_Price", "Product_Cost", "Product_Category", "Notes") FROM stdin;
    public          postgres    false    233   �5       Z          0    16794 	   Suppliers 
   TABLE DATA           a   COPY public."Suppliers" ("Id", "Supplier_Name", "PT_IVA", "Phone", "Email", "Notes") FROM stdin;
    public          postgres    false    221   �5       V          0    16772    UnitsOfMeasure 
   TABLE DATA           ?   COPY public."UnitsOfMeasure" ("Id", "Description") FROM stdin;
    public          postgres    false    217   	7       o           0    0 "   CategoryIngredientList_EntryId_seq    SEQUENCE SET     R   SELECT pg_catalog.setval('public."CategoryIngredientList_EntryId_seq"', 5, true);
          public          postgres    false    231            p           0    0    Ingredients_Format_Id_seq    SEQUENCE SET     K   SELECT pg_catalog.setval('public."Ingredients_Format_Id_seq"', 382, true);
          public          postgres    false    222            q           0    0    Ingredients_Id_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public."Ingredients_Id_seq"', 252, true);
          public          postgres    false    218            r           0    0    IsUsedValue_Id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public."IsUsedValue_Id_seq"', 8, true);
          public          postgres    false    215            s           0    0    OrderCategory_Id_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public."OrderCategory_Id_seq"', 3, true);
          public          postgres    false    230            t           0    0    OrdersList_Entry_Id_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public."OrdersList_Entry_Id_seq"', 1, false);
          public          postgres    false    226            u           0    0    Orders_Entry_Id_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public."Orders_Entry_Id_seq"', 1, false);
          public          postgres    false    224            v           0    0    Product_Composition_Id_seq    SEQUENCE SET     K   SELECT pg_catalog.setval('public."Product_Composition_Id_seq"', 13, true);
          public          postgres    false    234            w           0    0    Products_Id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public."Products_Id_seq"', 18, true);
          public          postgres    false    232            x           0    0    Suppliers_Id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public."Suppliers_Id_seq"', 29, true);
          public          postgres    false    220            y           0    0    UnitsOfMeasure_Id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public."UnitsOfMeasure_Id_seq"', 6, true);
          public          postgres    false    216            b   =   x�3�4�4�L�4���2R&F0�)�240�q��\� HYp�@8� ��@�	����� v
$      X   �  x��ZK�$�]眂{l�f�d�7XY�j��������7�% ���FK��+_�7�I���d2?=K*#���xlem�H�ݩ����x��{�F�Vu�\��j�~S���\.&�kuJ�l��Y�I@:�
'pڑIRv{bI��-�즞�̴��js��������I]V����_�OR<�n��iq!�~du;�$q�޼���V�I�>Z�����Q���?~v�DtTmsֵ��U��Ͽ�����}���Ͽ�����������?����#��&����g��'��S�DG�2�dnѝi��|,�ɋ���b=u�nc8�pյĉk�6$���LB��dI^:#M�jXwS�Þ�y�Z��z���̮XA��?�K$o
�Џ���3lht/[m����nK����k��Y����fړnds2�b�$+⨓yUE��׿Yr('�a�F�T�0}3ݼjhs'�q�9q�����	6�nB���䃊N�R{s�L��������)6u��/�\dX�{;݆�x�A�4���d�V��lg=ET�۟XY�aM��Ss���e��C�<�����~���;��r_E�T����+�p.��ŔF�tCZ���ОU냕R����U�i��Z���n+��p�UKn��|�N�757[���ף�@�I0��8�6Dp��I��H�D�Cy�1u�E��!e�!���s��z�aZe��͛^7z�	�` (����o���U�4�ԌNL��޲Gy�f� ����K}#���D$���q0�9�	�g�W�:�=ҧqԛ�1���C&����K[ӥ�L��d�e8�&ύ=���Ԫ�D��ӫ�H�/�����ѷ�lo^��K*J�s�@*�i|�%0��-ؘ�^���U��V�Ay�n,J[܋`��J��|fq�4з[c��lp���z�'���v�H����'���bQh* x������N�).[֑�}�BL�t�Ŀ�w{���ѝ&�ZU��5���&�b��BdIιɮW���KHw��D9:~"�6v������3�5(�Ɲ,JZ$D��'�|bح�:/��g���9w��2�v��1|.���u��=�$��w�p���w�Y�(	����!q��g�m ���=�ZKGΦ"��s���[���h�ݥ2A�<��p=�
U�����<mMQģ�A�$���ӓ�&���e��������N�W�	Sl��������1(KT�Q���U�x,�H7DDPv��Zr��E�oD�\��)ځ�ڎ�
p�N#1C3-8qQD�A�>�~DJ�SΨL_!�}��\C�l��|K*�T.\�Ut6��4O���q����ҳ��ǒ�("��7�k�~��P�R�.��Э������>R�����$*ӵV��X&��ן�we���j�Vg�'f?~z�������mˉ�!"�T��K'��&�-a���^��]V]��	�˲?0@�5`;�D/c7>Ҧ�Ep�;	6�C�,�<�����V�n9C�h��nSZ�r� �&�*��Y�D��s��;z]� ,@�[bٶ>ޖ��Ɉψ�2�JD_!1o�	��uMߴ�tMd�/o�j���vMU�ut;�Y��|�b��̢�����{k� �$���}?�_)�Ww�$�Ԧ�:��T�;t��ҵ�`�93�V$���E0 m\�pos2��
��"���X��86B���rIt5��H�[���c��$���Iq�e�@��+Ýܪ凄�-�W��������+�]9��I��Гf�&��%J`�	���S������O�+Z!@�B�1�B��{��>�IQi0�_*�Z�̂g�hl��������_��%l�^�겍����GEei)f�}ɕ�����w-�F�B8���Hsr4���Ɂ{'�b����_�R��B��:���MV/Z��n��}�"DL1:�(��ް.xDEw��C0%{�� ���#�#N� a��Nͦ1@��CT��X�*Y����j�Ʈ�&t�����'ʚie�t�ȃ�����2�"���"<+]-�Ye(Kv}��J
�
�����j�A�_���F ��I��{�+O
�8!p�p$bt�����uQ�%[!����LS���>TT�P^ƛ/O2r�?��w[9���5;���G�m�V��?Iv<~�],�S�u+;s�l�TѨ6|-;b��\�<-�xO_;�PpYR>i$�o8yT�����ݛ�ّ�V[2�t�Qخ�n}d���~#���k?ޕ��zQ�������σ�7����	;�����1w���嘛H�q��V_��J����ʊ!�� t�/L����E�����	ϩ�B�p@,&�|���g^v1���Z���8�X�ߒl���%��3��Bn��eQ7��8@����ۈɬ����(���Y8_2j�(�j5�ߏ'������	F!��wّ�d����;DDwg����v??~z�>���w^x���j��Y�nآ�&�;�=��~:8�-�š<�4������6����\�T/U�R���� L
X�v�4<G���CV�����&�!M�g|ІH^�H�	;U�×@mBj�\��^8y/;Av�k4��t҅P��C�_��[,|�c�;%�z����^GnuP�7K�5QXQШ̯.�
o��9�:Y��S�����M^T���R��r	+��{f.y����	�<�ߪ.�wu$4J���4|C7{v�r6�d��М������bύh���Rs�EBċ����=uǨ����$�8����	��d�~������znZ��g����� ��j��ZIϖ��P!��bl���t��:_u���eסּW|zd.�^xk��]ڧ�02��Df�L�������њ������_�H>U ;!�&?+G��6���.;�b�}!?��w(�Bdq�~�h �3G�if�wb��q�:��c���F��K��p��������cd�yw�kǇ<�"�Xջ�@������F�]bj��YM�ë'�-���pQ����
���^��V�nV�����Wm���yh�����ҷl	4����"D-jo{�=����9��?Og�@+$��N��F��?@:0�k����]g��c�A^�X��C�]�h����Xϭ[�,����^ ���\��r;� �B�����6�⋔b��_��#�T�'�dQ�naZg��\��G
��1h��k%4�N��rAs�������0fn�(���`��=%}���CUQ/�Ԯm�M�mC;�}�*�I��|��Q��a�ҳ���d)Qs�G>�A��������X8��S���t6S�v�F8{I���Nw����!>��̏��f�D
�?�\����E�i����2�&�
�\�f�s������a��L#O?U�$�[?��-4=E4_(�����&CSM������Ğ<<��MDm1�	���i�@��ұQ��YmT��]
��.Pi�����N��HES�y�_�ѶS`3����."{.w<�����v�P0,����W�^����      \   J  x��[[��&�V��:@�M�
�{V0�?�	W�.*�q)ڶ�$��.G>��������/�O����ٯ�FJg[i���L��Hᔅ�0Nx�R{+��vL4ב����9�+�+x�2�x�U������f�+/LN)�XNN�&B"UW��v�X{)H8�-k0���4���� �>�9��6�pW^��;V5V;s3�,7��XА,�WI;��d1�~�Е���p���6�[�	0�3�V^:�J:��`(����˗���g�s-�edlc垞�Mh�[�<��#NՎu�uƤ/�����WZ3���;�V
�|>�֏�������s��M �H�04�jK%�g�Sf���F��=,���,��q��ye����~Q[}v�X�ɩ�`˂���~^e�fV��J:���K�h5B/Uͳ��Y=/#�8���UȽ�u�X���𾊾�K9{���� l�����8n�rdW2�B�����fjD����]W�U(Mr{N7N;�y����(+�0c�o�y+rX��f���]�&Nn�uo�c%c��!SÝ3��x-�Z\�����e�拐y�}���E��"��!��H�c�����T	u1p�������0P���D�[����~}ɥb%g��{�I���)]�,�`3Ӧ3�f²����Bf�f�,��:��2x8#��v.�u9��o�:��^����+�j`�*�}�"E�8�2JW1tƛ��uyXP	��ӽ8�C���ӥ�߮��did��N\b�����2��B�h��>D�5��g3�������c�R-d�܌U�B}���:��s6�}pL'Df�����J+7�r��@�$��K��ۤ?����Q�"�o��AH�2τ��tt;*c���u�t)��$��ܶu��4+*���v�ؖu_:T�zM:KK[��+>��xa�4å���`�g1���?{^�ÁS,7^)���RI���b@�Ke��Et�g.<9T�bO�Х%������ig�I��N�>Ga730贖�{���!���Hm��y�����5e{АnG�6ӧ[w��V1�@��-|�wD�kj.ДxkKp��hZưt�w�����ѩ�{��<�?�莊j��L:֦���m��FA4�[��@k��P\��c<��3pAo�Q����6}�:]{Z׃��P~�!�.E���;.�5���-'3P\�Y�ET�O�õ���ߐ7uG�z�p{�G+e���`賟ʨ��Lt$i�&���n����E:Ҽ�<���91O�&4��-l5)G�&&A�΄�̨����{OAEKe��9�੔�z̮^��^�)+�e-y74-W��9�k�d3�����@�%-ǒ�z�Z���y	k�����`���v�Ue�
=S�d��k�m�ݓ��fz�j�S7����P�gF��*��і.�cQ�]r6�.);/��0t��\n���wm`Fh���G^.w.�Aau_���}h$h�.ܽ���M��4`��AP~�,������W͎+=�)*'T��m���8p�w^�(��/��
f�+��JRce���sΨ���e�L$�}Y��`�0�%R�7����w�3b����`�e��Ѥ+?�l�~Ki����|��)�qR��q�	^#(G������̐�j䦻MS����1�ʶ���\4�|Է�"�չ��3Q&�ܢ>q�O.�d�����Yk��o���uJ/�lt���l ��.��QՕ�;�nu}�;���!K'�����Ǟ�����d�A�`�5�^���d�׼4��h�}�yV� ���H�&�p��?�T���X���u%g7L��#a�%;��2���)	ͥ�XX�X�����Sලy�n vƂ�;�f�V9i�=�z�S���)���F@w���#����L,!{��孉�$M�.�6�5\~�d�d׶����<H;�Ȱ4�4��'�:��Y�h�(�9L��~���c�����9�vt�!�)���'��%��4j}�)2ѮYҺ�p~ޟ�bC�86�u���$X^[B��ů�����e�P���=z8ks�s�*IfmmMۂ+%k_�G�
�@���{�'���2U�����M���>,��^�#�5iD-�`�D�-�U�x]����b�5w������'j7)�"�s<��#����Si9����;���j4�G-�n�-x&��)f���H?C�.�G���`f^�ՑD�G�?���ĸW������2Qq���ݲq��ȥu����:}��bڿ��<��[c�Ee/����0|ǃ�����.�~�L�vԄ6���3m�Uny����� �h6���6k@Qez��a�;dt���}5��"˲T�P��ڃü.��5�����Q!�s��&���=Dֻk���ܟ�ǚ��n|�ĭq���TY�>s���atf}z
�?'���'�kk�#�N
w�����:��d��P�z���m1�N[�*�7���e����`V��[��6��z�_��p��;�I�4@6C�
5�^R�{Ӂö�����ߓ�!�BN�7��gkI�%�f�`sG���F��V��suI��y�aAd���k���j��]�=25^t)�����P���iI������ڱ��X{�>ڽi��xxDmUN��n�7-��p�+�3���w��s�c���??��F�g�g��r��S^]�XZ���c��=ly��X���yk�'z�/e��~�H�l38��Kp�Z��B�����3��p���#�a��z�jY����:i����X���p�娎�6�}�ʳ(v��X���q��E饽�>`�Ӫ�z�sg�0T�l5��≩M�����b��Q���דQFY��eOw����0zu�w��}�/�>�ce���t_a�X^�n?�Eq�S�4�b�Z��]6����~ȢT\Xy�]~?�a�K���
�hY�A���&�#�ߦO_�T�W7x��\�j.ޝ[q"K�,�id��`7l�6*��5,YJ"���zԌLyO�9�c!ɳ��d������Wu�\���ҟ��Jz]��/ᡥ���$��H�Yv}�@�!xח���� �ՈR�?_٢D�?�ɼ&�w*=��m��=�O�����UN�!����Լ�;`�//y<��Ax�4Q���%��H��FcPJ8���Wn��I]������7�����r���}�-�d�����Y�BY�<G��d�M�Bzog��Hz�g�9�nrY��9��RCb��187�`�/�+��)���͋T����?E�缢���y�{�[ߢ�X�O�i#���ULz=����sdI�M(=�]Ns�;��p�x�>�~:W��k��ڵ^N�<^KbN�>��n�v�S=�|��z�y�Ca7VU��Z��g����P'{�:pZ�g6=�b$כ�aM�����b��l�jݬ�5��q����*;��:���n�i����PD��U��`\�b�)�_q<l��6N���À��]������.���U
Q����<��J��M�nt�'ӭ�.�2��u�����n�7��Q����P?��ؐh?��=d�-��G���l¼�`��|?[���bY�,+�����/���i�X�{�Y�Oԛ�0���f��x�o�����������3�����?�B�R      S   i   x�%�1�@D�z|�= �!��ؑ�PQ���bD7�s�V:C�,n/�y�Z����1&/�1�^�Y[��P�j�.����s��Z\��:�	6��h�/�      a   B   x�3�I-.�tN,IM�/�LT�/J��KU(H-R(�p�(@)0QB5��b�1��v��qqq ��7      ^      x������ � �      `      x������ � �      h   <   x�M���0�jr�h��.�+��
8,h`��;�(�&n���ծ�m=<6������      f   >   x�34�H�����44�4�316�D�ΙI��1~\���E�y�@e@u&z�0m@�=... ��!      Z      x����n� ���vZԐ��G'q�5�J�� �&�ǽ�X/�4ɜ����ߴ`j�n�������u�w����G)x����+�����i���pq�����EtL�(�L~Y����d���#�F�E5њZR3Ye�����,�Q6�0$��C%���p5
z�)�g�#�Ҍ{@�W��� S��:c��ka���[z����U�z���cn�¥g�{��bp���M�܇:1�$��A �1Ȫ���-�֣�"#�m5��a��n�~�������o��Pl      V      x�3��	�2��v�2�������� .��     