   M S B A M L   `   `   `  ����@  <Hotel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Hotel.FormeHotel.Forme  5   	        Hotel.Forme.FrmRezervacija   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Hotel.Forme     5   	     � Title$  Dodaj/Izmeni rezervaciju��5   	   $	��300��6*   $	��300��67    � Icon$ /Forme/icon.jpg��6C   $
��Bold"�6Z    � 
ResizeMode$ NoResize=�6l    � WindowStartupLocation$ CenterScreen=�6�   .��5	      � .+�5
   
   �� 
�������.+�5      �� $A��;C:\Users\Andrijana\OneDrive\Radna površina\rezervacija.jpg��6   5      �� 5   
   $
��Gost��6   
�������
�������$
��Bold"�6J   $��1q�6\   
���**��$
��Left=�6�   $��	30,20,0,0q�6�   $	��Top=�6�   $	��106��6�   $��Center=�6�   �� 5   
   $��Radnik��6   
�������
�������$
��Bold"�6L   $��1q�6^   
���**��$
��Left=�6�   $��	30,50,0,0q�6�   $	��Top=�6�   $	��106��6�   $��Center=�6�   �� 5   
   $��Restoran��6   
�������
�������$
��Bold"�6N   $��1q�6`   
���**��$
��Left=�6�   $��	30,80,0,0q�6�   $	��Top=�6�   $	��106��6�   $��Center=�6�   �� 5   
   $	��Spa��6   
�������
�������$
��Bold"�6J   $��1q�6\   
���**��$
��Left=�6�   $��
30,110,0,0q�6�   $	��Top=�6�   $	��106��6�   $��Center=�6�   �� 5   
   $��Cena rezervacije��6   
�������
�������$
��Bold"�6V   $��1q�6h   
���**��$
��Left=�6�   $��
30,140,0,0q�6�   $	��Top=�6�   $	��106��6�   $��Center=�6�   �� 5   
   $��	Broj dana��6   
�������
�������$
��Bold"�6O   $��1q�6a   
���**��$
��Left=�6�   $��
30,170,0,0q�6�   $	��Top=�6�   $	��106��6�   $��Center=�6�   �� 5   
   -   5        $��cbGost��5       �� DisplayMemberPath$
 Gost��6!    �� SelectedValuePath$ GostID��6:   $
��Left=�6U   $��
140,20,0,0q�6p   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $��cbRadnik��5      $ Kontakt��6#   $ RadnikID��6?   $
��Left=�6\   $��
140,50,0,0q�6w   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $��
cbRestoran��5      $ Restoran��6%   $ 
RestoranID��6B   $
��Left=�6a   $��
140,80,0,0q�6|   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $��cbSpa��5      $	 Spa��6    $ SpaID��68   $
��Left=�6R   $��140,110,0,0q�6m   $	��Top=�6�   $	��120��6�   �� 5   
   -   5         ��Name$ txtCenaRezervacije��5      
�������
�������$��1q�6Y   $
��Left=�6m   $��140,140,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $�� ��6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ txtBrojDana��5      
�������
�������$��1q�6R   $
��Left=�6f   $��140,170,0,0q�6�   $
 Wrap=�6�   $�� ��6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $��
btnSacuvaj��5   �    � Cursor$
 Hand��6   
�������
�������
���**��$
��Bold"�6]   $��1q�6o   $��Sacuvaj��6�   $��
80,210,0,0q�6�   $	��Top=�6�   $
��Left=�6�   $��60��6  �� 5   
   -   5        $��	btnOtkazi��5   �   $
 Hand��6   
�������
�������
���**��$
��Bold"�6]   $��1q�6o   $��Otkazi��6�   $��
90,210,0,0q�6�   $	��Top=�6�   $��60��6�   5!      5"      