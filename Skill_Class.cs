using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SW_Character_Class;
using System.Windows;

namespace SW_Character_Class
{
    public class Skill_Class
    {
        #region // переменные класса
        private int score; // Величина навыка
        private int cost;
        private int skill_limit;
        private int Error_code;
        private string Error_msg;
        private int counter;
        private int skill_code;                     // Уникальный код для каждого умения
        private string skill_description;           // Текстовое описание умения
        private string path_read_description;       // Путь к файлу для вычитывания текстового описания навыка

        private int child_skill_limit,              // Возрастной лимит навыков для Ребенок
                    teen_skill_limit,               // Возрастной лимит навыков для Подросток
                    adult_skill_limit,              // Возрастной лимит навыков для Взрослый
                    middle_skill_limit,             // Возрастной лимит навыков для Средний
                    old_skill_limit,                // Возрастной лимит навыков для Пожилой
                    eldery_skill_limit,             // Возрастной лимит навыков для Почтенный
                    unknown_age_status_skill_limit; // Возрастной лимит навыков для Неизвестный возрастной статус

        private int age_skill_limit,                // Текущий возрастной лимит навыка
                    range_skill_limit;              // Текущий лимит навыка, исходя из ранга персонажа

        private int private_skill_limit,            // Лимит прокачки навыков для Рядовой
                    veteran_skill_limit,            // Лимит прокачки навыков для Ветеран
                    hero_skill_limit,               // Лимит прокачки навыков для Герой
                    epic_skill_limit,               // Лимит прокачки навыков для Эпик
                    immortal_skill_limit;           // Лимит прокачки навыков для Бессмертный

        private bool is_combat_skill;               // Флаг, является ли навык боевым умением. 1 - является, 0 - не является


        private SW_Character _SW_Char;
        #endregion

        #region // конструктор класса

        public Skill_Class( SW_Character SW_Char)
        {
            _SW_Char = SW_Char;
            counter = 0;
            Set_Error_Code(0);
        }

        #endregion

        #region //методы класса

        #region // Методы установки счетчика прокачки умения для контроля за расовыми бонусами умения
        public void Set_counter (int insert_value) { counter = insert_value; }
        public int Get_counter() { return counter; }
        private void Increase_counter() { counter = counter + 1; }
        private void Decrease_counter() { counter = counter - 1; }
        #endregion
         
        #region // Методы установки стоимости умения
        public void Set_cost(int insert_value) { cost = insert_value; }
        public int Get_cost() { return cost; }
        #endregion

        #region // Методы установки значения умения навыка
        public void Set_score(int insert_value) { score = insert_value; }
        public int Get_score() { return score; }
        #endregion


        // Устнаваливаем код ошибки для выведения сообщения
        private void Set_Error_Code(int error_code) { Error_code = error_code; } 

        // формируем сообщение по сформированному коду ошибки
        public string Get_Error_msg() 
        {
            switch (Error_code)
            {
                case 0:
                    Error_msg = "";
                    break;
                case 1:
                    Error_msg = "Превышение лимита очков умений для вашего персонажа!";
                    break;
                case 2:
                    Error_msg = "Невозможно опустить значение умения персонажа ниже 0!";
                    break;
                case 3:
                    Error_msg = "Недостаточно очков опыта для изучения данного навыка!";
                    break;

            }
            
            return Error_msg; 
        }

        // Метод увеличения навыка на 1
        public void Score_Increase()  
        {
            Skill_limit(Get_age_skill_limit(),Get_range_skill_limit());                         // Получаем лимит прокачки умения

            if (score < Get_Skill_limit())                                            // Проводим проверку на превышение лимита прокачки
            {
                if (_SW_Char.Get_experience() >= Get_cost())
                {
                    Set_score(Get_score() + 1);
                    Increase_counter();                                             // Увеличиваем счетчик прокачек умения персонажа
                    _SW_Char.Decrease_experience_on(Get_cost());                    // выражение для уменьшения значения очков опыта
                    Set_Error_Code(0);                                              // Снимаем код ошибки
                }
                else
                {
                    Set_Error_Code(3);
                }
            }
            else
            {
                Set_Error_Code(1);
            }
        }

        // Метод уменьшения навыка на 1
        public void Score_Decrease()  
        {
            if (score > 0)
            {
                Set_score(Get_score() - 1);
                Decrease_counter();
                _SW_Char.Increase_experience_on(Get_cost());                    // выражение для увеличения значения очков опыта
                Set_Error_Code(0);                                              // Снимаем код ошибки
            }
            else
            {
                Set_Error_Code(2);
            }
        }
        #region // методы установки возрастных лимитов прокачки навыка
        // Устанавливаем значение возрастного лимита умения для Ребеной
        public void Set_child_skill_limit(int insert_int) { child_skill_limit = insert_int; }
        // Предоставление значение возрастного лимита умения для Ребенок
        public int Get_child_skill_limit() { return child_skill_limit; }
        // Устанавливаем значение возрастного лимита умения для Подросток
        public void Set_teen_skill_limit(int insert_int) { teen_skill_limit = insert_int; }
        // Предоставление значение возрастного лимита умения для Подросток
        public int Get_teen_skill_limit() { return teen_skill_limit; }
        // Устанавливаем значение возрастного лимита умения для Взрослый
        public void Set_adult_skill_limit(int insert_int) { adult_skill_limit = insert_int; }
        // Предоставление значение возрастного лимита умения для Взрослый
        public int Get_adult_skill_limit() { return adult_skill_limit; }
        // Устанавливаем значение возрастного лимита умения для Средний
        public void Set_middle_skill_limit(int insert_int) { middle_skill_limit = insert_int; }
        // Предоставление значение возрастного лимита умения для Средний
        public int Get_middle_skill_limit() { return middle_skill_limit; }
        // Устанавливаем значение возрастного лимита умения для Пожилой
        public void Set_old_skill_limit(int insert_int) { old_skill_limit = insert_int; }
        // Предоставление значение возрастного лимита умения для Пожилой
        public int Get_old_skill_limit() { return old_skill_limit; }
        // Устанавливаем значение возрастного лимита умения для Почтенный
        public void Set_eldery_skill_limit(int insert_int) { eldery_skill_limit = insert_int; }
        // Предоставление значение возрастного лимита умения для Почтенный
        public int Get_eldery_skill_limit() { return eldery_skill_limit; }
        // Устанавливаем значение возрастного лимита умения для Неизвестный возрастной статус
        public void Set_unknown_age_status_skill_limit(int insert_int) { unknown_age_status_skill_limit = insert_int; }
        // Предоставление значение возрастного лимита умения для Неизвестный возрастной статус
        public int Get_unknown_age_status_skill_limit() { return unknown_age_status_skill_limit; }
        #endregion
        // Метод определения лимита прокачки навыка относительно возраста персонажа
        public void Set_age_skill_limit(string age_status)
        {
            _SW_Char.Get_age_status();
            foreach (Age_status_class Age_status in _SW_Char._Age_statuses)
            {
                if (Age_status.Get_age_status_name() == age_status)
                {
                    switch (Age_status.Get_age_status_code())
                    {
                        case (int)SW_Character.enum_Age_status.Child:   age_skill_limit = Get_child_skill_limit(); break; //2
                        case (int)SW_Character.enum_Age_status.Teen:    age_skill_limit = Get_teen_skill_limit(); break; //4
                        case (int)SW_Character.enum_Age_status.Adult:   age_skill_limit = Get_adult_skill_limit(); break; //10
                        case (int)SW_Character.enum_Age_status.Middle:  age_skill_limit = Get_middle_skill_limit(); break; //10
                        case (int)SW_Character.enum_Age_status.Old:     age_skill_limit = Get_old_skill_limit(); break; //10
                        case (int)SW_Character.enum_Age_status.Eldery:  age_skill_limit = Get_eldery_skill_limit(); break; //10
                        case (int)SW_Character.enum_Age_status.Unknown: age_skill_limit = Get_unknown_age_status_skill_limit(); break; //0
                    }
                    break;
                }
            }
        }
        // Предоставляем значение возрасттного лиимита прокачки навыка
        public int Get_age_skill_limit() { return age_skill_limit; }

        #region // методы лимиты прокачки навыка персонажа в относительности от ранга персонажа
        // Устаниваливаем значение лимита прокачки навыка для Рядовой
        public void Set_private_skill_limit(int insert_int) { private_skill_limit = insert_int; }
        // Предоставляем значение лимита прокачки навыка для Рядовой
        public int Get_private_skill_limit() { return private_skill_limit; }
        // Устаниваливаем значение лимита прокачки навыка для Ветеран
        public void Set_veteran_skill_limit(int insert_int) { veteran_skill_limit = insert_int; }
        // Предоставляем значение лимита прокачки навыка для Ветеран
        public int Get_veteran_skill_limit() { return veteran_skill_limit; }
        // Устаниваливаем значение лимита прокачки навыка для Герой
        public void Set_hero_skill_limit(int insert_int) { hero_skill_limit = insert_int; }
        // Предоставляем значение лимита прокачки навыка для Герой
        public int Get_hero_skill_limit() { return hero_skill_limit; }
        // Устаниваливаем значение лимита прокачки навыка для Эпик
        public void Set_epic_skill_limit(int insert_int) { epic_skill_limit = insert_int; }
        // Предоставляем значение лимита прокачки навыка для Эпик
        public int Get_epic_skill_limit() { return epic_skill_limit; }
        // Устаниваливаем значение лимита прокачки навыка для Бессмертный
        public void Set_immortal_skill_limit(int insert_int) { immortal_skill_limit = insert_int; }
        // Предоставляем значение лимита прокачки навыка для Бессмертный
        public int Get_immortal_skill_limit() { return immortal_skill_limit; }
        #endregion
        // Устанавливаем лимит прокачка навыка оотносительно ранга персонажа 
        public void Set_range_skill_limit(string range)
        {
            foreach (Range_Class Range in _SW_Char._Ranges)
            {
                if(Range.Get_range_name() == range)
                {
                    switch(Range.Get_range_code())
                    {
                        case (int)SW_Character.enum_Range.Private: 
                            range_skill_limit = Get_private_skill_limit(); 
                            break;
                        case (int)SW_Character.enum_Range.Veteran:
                            range_skill_limit = Get_veteran_skill_limit();
                            break;
                        case (int)SW_Character.enum_Range.Hero:
                            range_skill_limit = Get_hero_skill_limit();
                            break;
                        case (int)SW_Character.enum_Range.Epic:
                            range_skill_limit = Get_epic_skill_limit();
                            break;
                        case (int)SW_Character.enum_Range.Immortal:
                            range_skill_limit = Get_immortal_skill_limit();
                            break;
                    }

                }
            }
        }
        // Предоставляем значение лимита прокачки навыка относительно ранга персонажа
        public int Get_range_skill_limit() { return range_skill_limit; }
        // Устанавливаем флаг, является ли навык боевым умением
        public void Set_is_combat_skill() { is_combat_skill = true; }
        public void Reset_is_combat_skill() { is_combat_skill = false; }
        // Предоставляем значения флага боевого умения
        public bool Get_is_combat_skill() { return is_combat_skill; }

        #region //Сравнение лимитов навыков. Используем меньший лимит
        public int Skill_limit(int age_limit, int range_limit)
        {
            if (age_limit >= range_limit)
            {
                skill_limit = range_limit;
            }
            else if (range_limit > age_limit)
            {
                skill_limit = age_limit;
            }
            else
            {
                skill_limit = 0;
            }
            return skill_limit;
        }

        public int Get_Skill_limit() { return skill_limit; }

        public void Set_skill_code(int insert_int) { skill_code = insert_int; }
        public int Get_skill_code() { return skill_code; }
        #endregion

        // Устанавливаем путь к текстовому файлу с описанием навыка
        public void Set_path_read_description(string input_text) { path_read_description = input_text; }
        // Предоставляем путь к текстовому файлу с описанием навыка
        public string Get_path_read_description() { return path_read_description; }
        // Устанавливаем текстовое описание навыка персонажа
        public void Set_skill_description(string insert_text) { skill_description = insert_text; }
        // Предоставляем текстовое описание навыка персонажа
        public string Get_skill_description() { return skill_description; }
        #endregion
    }
}
