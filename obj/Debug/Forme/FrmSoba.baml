   M S B A M L   `   `   `  ����@  <Hotel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Hotel.FormeHotel.Forme  5   	       Hotel.Forme.FrmSoba   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Hotel.Forme     5   	     � Title$  Dodaj/Izmeni sobu��5   	   $	��240��6$   $	��250��61    � Icon$ /Forme/icon.jpg��6=   $
��Bold"�6T    � 
ResizeMode$ NoResize=�6f    � WindowStartupLocation$ CenterScreen=�6|   .��5	      � .+�5
   
   �� 
�������.+�5      �� $:��4C:\Users\Andrijana\OneDrive\Radna površina\soba.jpg��6   $	��228��6Q   $	��362��6^   5      �� 5   
   
�������
�������$
��Bold"�6;   $��1q�6M   
���**��$��	Broj sobe��6t   $
��Left=�6�   $��	30,20,0,0q�6�   $	��Top=�6�   $��76��6�   $��Center=�6�   �� 5   
   
�������
�������$
��Bold"�6;   $��1q�6M   
���**��$��Sprat��6t   $
��Left=�6�   $��	30,50,0,0q�6�   $	��Top=�6�   $��76��6�   $��Center=�6�   �� 5   
   
�������
�������$
��Bold"�6;   $��1q�6M   
���**��$��
Vrsta sobe��6t   $
��Left=�6�   $��	30,80,0,0q�6�   $	��Top=�6�   $��76��6�   $��Center=�6�   �� 5   
   
�������
�������$
��Bold"�6;   $��1q�6M   
���**��$��Status sobe��6t   $
��Left=�6�   $��
30,110,0,0q�6�   $	��Top=�6�   $��76��6�   $��Center=�6�   �� 5   
   
�������
�������$
��Bold"�6;   $��1q�6M   
���**��$��Rezervacija��6t   $
��Left=�6�   $��
30,140,0,0q�6�   $	��Top=�6�   $��76��6�   $��Center=�6�   �� 5   
   -   5        $��txtBrojSobe��5   Q   
�������
�������$��1q�6=   $
��Left=�6d   $��
110,20,0,0q�6    �� TextWrapping$
 Wrap=�6�   $�� ��6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $��txtSprat��5   Q   
�������
�������$��1q�6=   $
��Left=�6a   $��
110,50,0,0q�6|   $
 Wrap=�6�   $�� ��6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $��cbVrstaSobe��5       �� DisplayMemberPath$ Naziv��6&    �� SelectedValuePath$ VrstaSobeID��6@   $
��Left=�6a   $��
110,80,0,0q�6|   $	��Top=�6�   $	��120��6�   �� 5   
   -   5         ��Name$ cbStatusSobe��5      
�������
�������$��1q�6T   $�� ��6h   $
��Left=�6s   $��130,110,0,0q�6�   $	��Top=�6�   �� 5   
   -   5        $��cbRezervacija��5      $
 Dani��6*   $ RezervacijaID��6C   $
��Left=�6e   $��110,140,0,0q�6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $��
btnSacuvaj��5   �    � Cursor$
 Hand��6   
�������
�������
���**��$
��Bold"�6]   $��1q�6o   $��Sacuvaj��6�   $
��Left=�6�   $��
60,177,0,0q�6�   $	��Top=�6�   $��50��6  �� 5   
   -   5        $��	btnOtkazi��5   �   $
 Hand��6   
�������
�������
���**��$
��Bold"�6]   $��1q�6o   $��Otkazi��6�   $��
90,177,0,0q�6�   $	��Top=�6�   $��50��6�   5      5      