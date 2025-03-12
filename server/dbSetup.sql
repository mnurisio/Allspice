CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name VARCHAR(255) COMMENT 'User Name',
    email VARCHAR(255) UNIQUE COMMENT 'User Email',
    picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

CREATE TABLE recipes (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    title TINYTEXT NOT NULL,
    instructions TEXT,
    img TEXT NOT NULL,
    category ENUM(
        'breakfast',
        'lunch',
        'dinner',
        'snack',
        'dessert'
    ) NOT NULL,
    creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (creator_id) REFERENCES accounts (id) ON DELETE CASCADE
)

CREATE TABLE ingredients (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    name TINYTEXT NOT NULL,
    quantity TINYTEXT NOT NULL,
    recipe_id INT NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES recipes (id) ON DELETE CASCADE
)

CREATE TABLE favorites (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    recipe_id INT NOT NULL,
    account_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES recipes (id) ON DELETE CASCADE,
    FOREIGN KEY (account_id) REFERENCES accounts (id) ON DELETE CASCADE
)

INSERT INTO
favorites(recipe_id, account_id)
VALUES(@recipeId, @accountId);
SELECT
accounts.*,
favorites.*
FROM favorites
JOIN accounts ON favorites.account_id = accounts.id
WHERE favorites.id = LAST_INSERT_ID()

SELECT
        favorites.*,
        recipes.*,
        accounts.*
        FROM favorites
        JOIN recipes ON favorites.recipe_id = recipes.id
        JOIN accounts ON favorites.account_id = accounts.id
        WHERE favorites.id = LAST_INSERT_ID();

INSERT INTO
        favorites(recipe_id, account_id)
        VALUES(@recipeId, @accountId);

        SELECT
        favorites.*,
        profile_view.*,
        recipes.*
        FROM favorites
        JOIN profile_view ON favorites.account_id = profile_view.id
        JOIN recipes ON favorites.recipe_id = recipes.id
        WHERE favorites.id = LAST_INSERT_ID()

DROP TABLE ingredients

SELECT * FROM favorites

SELECT ingredients.*, recipes.*
FROM ingredients
    JOIN recipes ON ingredients.recipe_id = recipes.id
WHERE
    ingredients.id = LAST_INSERT_ID();

SELECT
ingredients.*,
recipes.*,
accounts.*
FROM ingredients
JOIN recipes ON recipes.id = ingredients.recipe_id
JOIN accounts ON accounts.id = recipes.creator_id
WHERE recipe_id = 27


SELECT 
        ingredients.*,
        recipes.* 
        FROM ingredients 
        JOIN recipes ON recipes.id = ingredients.recipe_id
        WHERE ingredients.id = 3


        SELECT * From recipes