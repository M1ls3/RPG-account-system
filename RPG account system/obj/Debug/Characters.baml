   M S B A M L   `   `   `  ����M  IRPG account system, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:RPG_account_systemRPG_account_system  5   	   #    RPG_account_system.Characters   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:RPG_account_system     5   	     � Title$  
Characters��5   	   $	��450��6   $	��654��6)   ��5	      �� 5
   
    �� ImageSource$ /CharBack.jpg��6    j� Stretch$ UniformToFill=�61   .��5      � $��-7,0,7,0q�6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGrid_Characters��5      $��	5,0,-6,51q�6[   
���   �   RowBackground �	#FFCC9F5E   AlternatingRowBackground �	#FFBD823F��6n   Z� ��6�   Z� 6�   �� 5   
   -   5         ��Name$ button_Char_back��5      $��<Back��6+   $
��Left=�6>   $��28��6Y   $��
10,388,0,0q�6e   $	��Top=�6y   $��95��6�   ���	#FF29120B���	#FFAB8C7D$��Cooper Black)�6�   $��22(�6�   6�   �� 5   
   -   5        $ button_Char_Create��5      $��Create��6-   $
��Left=�6>   $��28��6Y   $��327,388,0,0q�6e   $	��Top=�6z   $��95��6�   ���	#FF29120B���	#FFAB8C7D$��Cooper Black)�6�   $��22(�6�   6�   �� 5   
   -   5        $ button_Char_Edit��5      $
��Edit��6+   $
��Left=�6:   $��28��6U   $��439,388,0,0q�6a   $	��Top=�6v   $��95��6�   ���	#FF29120B���	#FFAB8C7D$��Cooper Black)�6�   $��22(�6�   6�   �� 5   
   -   5        $ button_Char_Delete��5      $��Delete��6-   $
��Left=�6>   $��28��6Y   $��549,388,0,0q�6e   $	��Top=�6z   $��95��6�   ���	#FF29120B���	#FFAB8C7D$��Cooper Black)�6�   $��22(�6�   6�   �� 5   
   -   5        $ button_Char_location��5      $��Location��6/   $
��Left=�6B   $��28��6]   $��120,388,0,0q�6i   $	��Top=�6~   $	��115��6�   ���	#FF29120B���	#FFAB8C7D$��Cooper Black)�6�   $��22(�6�   6�   5      5      